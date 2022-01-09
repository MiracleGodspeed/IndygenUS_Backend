using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BusinessLayer.Interface;
using DataLayer.Dtos;
using DataLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;

namespace BusinessLayer.Services
{
    public class UserService : Repository<User>, IUserService
    {
        //private readonly ELearnContext _context;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;
        //private readonly string defualtPassword = "1234567";
        private readonly string key;

        ResponseModel response = new ResponseModel();

        public UserService(DBContext context, IConfiguration configuration)
             : base(context)
        {
           // _context = context;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");
            key = _configuration.GetValue<string>("AppSettings:Key");


        }

        public async Task<UserDto> AuthenticateUser(LoginDto dto, string injectkey)
        {
            try
            {

                UserDto userDto = new UserDto();
                var user = await _context.USER
                   .Include(r => r.Role)
                   .Include(r => r.Person)
                   .Where(f => f.Active && f.Username == dto.UserName).FirstOrDefaultAsync();
                var getRegion = await _context.USER_FRINGE_DETAILS.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();

                if (user == null)
                    return null;
                //if (!user.IsVerified)
                //    throw new Exception("Account has not been verified!");
                if (!VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
                    return null;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(injectkey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.Name),

                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };
                user.LastLogin = DateTime.UtcNow;
                _context.Update(user);
                await _context.SaveChangesAsync();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                userDto.Token = tokenHandler.WriteToken(token);
                userDto.Password = null;
                userDto.UserName = user.Username;
                userDto.RoleName = user.Role.Name;
                userDto.UserId = user.Id;
                userDto.PersonId = user.PersonId;
                userDto.SecurityQuestion = user.IsAnsweredSecurityQuestion;
                userDto.IsVerified = user.IsVerified;
                userDto.IsUpdatedProfile = user.IsUpdatedProfile;
                userDto.FullName = user.Person.Surname + " " + user.Person.Firstname;
                userDto.RegionId = getRegion.RegionId;

                return userDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDto> NewUser(RegisterDto loginDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if(loginDto.UserName != null)
                {
                    var doesExist = await _context.USER.Where(x => x.Username == loginDto.UserName).FirstOrDefaultAsync();
                    if (doesExist != null)
                        throw new NullReferenceException("user already exists");
                    Person person = new Person()
                    {
                        Surname = "-",
                        Firstname = loginDto.UserName,
                        //Othername = "-",
                        Email = loginDto.UserName
                    };
                    _context.Add(person);
                    await _context.SaveChangesAsync();

                    Utility.CreatePasswordHash(loginDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    var genUserId = Convert.ToString(Guid.NewGuid());
                    User user = new User()
                    {
                        Id = genUserId,
                        Username = loginDto.UserName,
                        LastLogin = DateTime.Now,
                        SignUpDate = DateTime.Now,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        IsVerified = false,
                        RoleId = 1,
                        Active = true,
                        PersonId = person.Id
                    };
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    UserFringeDetails fringeDetails = new UserFringeDetails()
                    {
                        UserId = user.Id,
                        RegionId = loginDto.RegionId
                        //NationalityId = loginDto.NationalityId
                    };
                    _context.Add(fringeDetails);
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
                LoginDto dto = new LoginDto()
                {
                    UserName = loginDto.UserName,
                    Password = loginDto.Password
                };
                return await AuthenticateUser(dto, key);
            }

            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }



        public async Task<int> UpdateUserProfile(UpdateProfileDto dto, string userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var getUser = await _context.USER.Where(x => x.Id == userId).FirstOrDefaultAsync();
                if (getUser == null)
                    throw new NullReferenceException("user not found");
                Person person = await _context.PERSON.Where(x => x.Id == getUser.PersonId).FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(dto.Firstname))
                {
                    person.Firstname = dto.Firstname;
                }
                if (!string.IsNullOrEmpty(dto.Lastname))
                {
                    person.Surname = dto.Lastname;
                }
                if (!string.IsNullOrEmpty(dto.Email))
                {
                    person.Email = dto.Email;
                }
                if (dto.Dob != null)
                {
                    person.DateOfBirth = dto.Dob;
                }
                person.Active = true;
                if (!string.IsNullOrEmpty(dto.MobileNumber))
                {
                    person.PhoneNo = dto.MobileNumber;
                }
                _context.Update(person);

                UserFringeDetails fringeDetails = await _context.USER_FRINGE_DETAILS.Where(x => x.UserId == getUser.Id).FirstOrDefaultAsync();

                fringeDetails.IsTextMessageContact = dto.IsTextMessageContact;
                if(dto.AncestryId > 0)
                {
                    fringeDetails.AncestryIdentityId = dto.AncestryId;

                }
                if (dto.SalivaBloodResponse > 0)
                {
                    fringeDetails.SalivaBloodSharingId = dto.SalivaBloodResponse;

                }
                if (dto.ClinicalTrialsResponse > 0)
                {
                    fringeDetails.ClinicalTrialsId = dto.ClinicalTrialsResponse;

                }
                if (dto.NationalityId > 0)
                {
                    fringeDetails.NationalityId = dto.NationalityId;

                }
                if (dto.RegionId > 0)
                {
                    fringeDetails.RegionId = dto.RegionId;

                }
                if (dto.PlatformDiscoveryId > 0)
                {
                    fringeDetails.PlatformDiscoveryTypeId = dto.PlatformDiscoveryId;

                }
                if (dto.ReferalName != null)
                {
                    fringeDetails.ReferalPersonName = dto.ReferalName;

                }
                if (dto.Weight != null)
                {
                    fringeDetails.Weight = dto.Weight;

                }
                if (dto.Height != null)
                {
                    fringeDetails.Height = dto.Height;

                }
                if (dto.SexualOrientationId > 0)
                {
                    fringeDetails.SexualOrientationId = dto.SexualOrientationId;

                }
                if (dto.ArmedForceVeteranResponse > 0)
                {
                    fringeDetails.ArmedForceVeteranId = dto.ArmedForceVeteranResponse;

                }
                if (dto.MemberBlackCommunityResponse > 0)
                {
                    fringeDetails.MemberBlackCommunityId = dto.MemberBlackCommunityResponse;

                }
                if (dto.GenderId > 0)
                {
                    fringeDetails.GenderId = dto.GenderId;

                }

                _context.Update(fringeDetails);
                if(dto.Email != null && getUser.Username != dto.Email)
                {
                    getUser.Username = dto.Email;
                }
                getUser.IsUpdatedProfile = true;
                _context.Update(getUser);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }


        public async Task<UpdateProfileDto> GetUserProfile(string userId)
        {
            try
            {
                var getUser = await _context.USER.Where(x => x.Id == userId).Include(x => x.Person).FirstOrDefaultAsync();
                if (getUser == null)
                    throw new NullReferenceException("user not found");
                
                var fringeDetails = await _context.USER_FRINGE_DETAILS.Where(x => x.UserId == userId).FirstOrDefaultAsync();

                UpdateProfileDto responseDto = new UpdateProfileDto()
                {
                    IsTextMessageContact = fringeDetails.IsTextMessageContact,
                    AncestryId = fringeDetails.AncestryIdentityId,
                    SalivaBloodResponse = fringeDetails.SalivaBloodSharingId,
                    ClinicalTrialsResponse = fringeDetails.ClinicalTrialsId,
                    NationalityId = fringeDetails.NationalityId,
                    PlatformDiscoveryId = fringeDetails.PlatformDiscoveryTypeId,
                    ReferalName = fringeDetails.ReferalPersonName,
                    Weight = fringeDetails.Weight,
                    Height = fringeDetails.Height,
                    SexualOrientationId = fringeDetails.SexualOrientationId,
                    MemberBlackCommunityResponse = fringeDetails.MemberBlackCommunityId,
                    Firstname = getUser.Person.Firstname,
                    Lastname = getUser.Person.Surname,
                    Email = getUser.Person.Email,
                    ArmedForceVeteranResponse = fringeDetails.ArmedForceVeteranId,
                    Dob = getUser.Person.DateOfBirth,
                    GenderId = fringeDetails.GenderId,
                    MobileNumber = getUser.Person.PhoneNo,
                    RegionId = fringeDetails.RegionId
                };
                return responseDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> PostUserAgreement(string userId, int agreementType)
        {
            try
            {
                var getUser = await _context.USER.Where(x => x.Id == userId).FirstOrDefaultAsync();
                if (getUser == null)
                    throw new NullReferenceException("invalid user");
                if (userId != null && agreementType > 0)
                {
                    var isAgreed = await _context.USER_COMPLIANCE.Where(x => x.UserId == userId && x.ComplianceTypeId == agreementType).FirstOrDefaultAsync();
                    if (isAgreed != null)
                        throw new Exception("user already agreed to this");
                    UserCompliance userCompliance = new UserCompliance()
                    {
                        UserId = userId,
                        ComplianceTypeId = agreementType,
                        DateEntered = DateTime.Now,
                        Active = true
                    };
                    _context.Add(userCompliance);
                    await _context.SaveChangesAsync();
                }
                return StatusCodes.Status200OK;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BaseDto>> GetUserComplianceType(string userId)
        {
            try
            {
                return await _context.USER_COMPLIANCE.Where(x => x.UserId == userId).Include(x => x.ComplianceType)
                    .Select(f => new BaseDto
                    {
                        Id = f.ComplianceTypeId,
                        Name = f.ComplianceType.Name
                    })
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        static void EncryptAesManaged(string raw)
        {
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    // Encrypt string    
                    byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                    //var ff = $"Encrypted data:{Encoding.UTF8.GetString(encrypted)}";
                    // Decrypt the bytes to a string.
                    string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                   
                }
            }
            catch (Exception exp)
            {
                //Console.WriteLine(exp.Message);
            }
            Console.ReadKey();
        }
        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {                    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        //public async Task<int> ResetPassword(string Username)
        //{
        //    try
        //    {
        //        var _username = Username.Trim();
        //        var getUser = await _context.USER.Where(x => x.Username == _username.Trim()).FirstOrDefaultAsync();
        //        if(getUser != null)
        //        {                   
        //            string generateGuid = Convert.ToString(Guid.NewGuid());
        //            var splitGuid = generateGuid.Split("-");
        //            var guid = splitGuid[1];
        //            getUser.Guid = guid;
        //            _context.Update(getUser);
        //            await _context.SaveChangesAsync();
        //            return StatusCodes.Status200OK;
        //        }
        //        return 0;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


      

        public static IRestResponse IMailerGun(string to, string subject, string _body)
        {
            //string _body = "Your Verification code is : " + body;
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3/");
            client.Authenticator =
                new HttpBasicAuthenticator("api", "key-92d7677d9841bc85e2389a1e31b7533c");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "tarveninvestment.com", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Tarven Investment <mailgun@tarven.com>");
            request.AddParameter("to", to);
            //request.AddParameter("to", "YOU@YOUR_DOMAIN_NAME");
            request.AddParameter("subject", subject);
            request.AddParameter("text", _body);
            request.Method = Method.POST;
            var stat = client.Execute(request);
            return stat;
        }

        public string EncryptAuthenticationTokenAes(string plainText)
        {

            byte[] encrypted;
            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                aesAlg.Padding = PaddingMode.None;
                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);

        }


        public string DecryptPasswordAes(string encryptedString)
        {
            //Convert cipher text back to byte array
            byte[] cipherText = Convert.FromBase64String(encryptedString);

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesManaged object
            // with the specified key and IV.
            AesManaged aesAlg = new AesManaged();
            //AesManaged aes = new AesManaged();
            aesAlg.Padding = PaddingMode.Zeros;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            

            return plaintext;

        }
        public async Task<int> PostSecurityQuestions(string userId, List<BaseDto> dto)
        {
            try
            {
                if(dto != null && dto.Count > 0)
                {
                    var getUser = await _context.USER.Where(x => x.Id == userId).FirstOrDefaultAsync();
                    if (getUser == null)
                        throw new NullReferenceException("user not found");
                    foreach(var item in dto)
                    {
                        var isEntered = await _context.USER_SECURITY_QUESTIONS.Where(x => x.SecurityQuestionId == item.Id && x.UserId == userId).FirstOrDefaultAsync();
                        if(isEntered == null)
                        {
                            UserSecurityQuestions securityQuestions = new UserSecurityQuestions()
                            {
                                UserId = userId,
                                SecurityQuestionId = Convert.ToInt32(item.Id),
                                SecurityAnswer = item.Name,
                                Active = true
                            };
                            _context.Add(securityQuestions);
                            await _context.SaveChangesAsync();
                        }
                    }

                    getUser.IsAnsweredSecurityQuestion = true;
                    _context.Update(getUser);
                    await _context.SaveChangesAsync();
                    return StatusCodes.Status200OK;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> ResetPassword(string email)
        {
            try
            {
                var _email = email.Trim();
                string generateGuid = Convert.ToString(Guid.NewGuid());
                var splitGuid = generateGuid.Split("-");
                var guid = splitGuid[1];
                var getUser = await _context.USER.Where(d => d.Username == _email).Include(p => p.Person).FirstOrDefaultAsync();
                if (getUser != null)
                {
                    Random generator = new Random();
                    var otp = generator.Next(0, 1000000).ToString("D6");
                    getUser.Guid = otp;
                    getUser.IsVerified = false;
                    _context.Update(getUser);
                    await _context.SaveChangesAsync();
                    SendSimpleMessage(email, "Password Reset", otp);
                    return StatusCodes.Status200OK;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> ConfirmResetPassword(string email, string code, string password)
        {
            try
            {
                var _email = email.Trim();
                var _code = code.Trim();
                var getUser = await _context.USER.Where(d => d.Username == _email && !d.IsVerified && d.Guid == _code).FirstOrDefaultAsync();

                //if (!VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
                if (getUser != null)
                {
                    Utility.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                    getUser.IsVerified = true;
                    getUser.Guid = null;
                    getUser.PasswordHash = passwordHash;
                    getUser.PasswordSalt = passwordSalt;
                    _context.Update(getUser);
                    await _context.SaveChangesAsync();
                    return StatusCodes.Status200OK;
                }
                else
                {
                    throw new NullReferenceException("OTP provided is invalid. Try again");
                }
                //return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> ChangePassword(ChangePasswordDto dto)
        {
            try
            {
                if (!String.IsNullOrEmpty(dto.NewPassword))
                {
                    var user = await _context.USER
                  .Include(r => r.Role)
                  .Include(r => r.Person)
                  .Where(f => f.Active && f.Id == dto.UserId).FirstOrDefaultAsync();
                    if (user == null)
                        throw new NullReferenceException("Error updating password");
                    if (!VerifyPasswordHash(dto.OldPassword, user.PasswordHash, user.PasswordSalt))
                        throw new NullReferenceException("Old password provided is not a match. Double check and try again.");
                    Utility.CreatePasswordHash(dto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                }
                return StatusCodes.Status200OK;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IRestResponse SendSimpleMessage(string to, string subject, string _guid)
        {
            //string _template = "APIs.Resources.emailTemplate.cshtml";
            var currDirectory = Directory.GetCurrentDirectory();

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3/");
            client.Authenticator =
                new HttpBasicAuthenticator("api", "key-270ef6d7207a4da37f4bf1ecad5fc25b");


            RestRequest request = new RestRequest();
            request.AddParameter("domain", "byusforall.com", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "IndyGeneUs Health <mailgun@byusforall.com>");
            request.AddParameter("to", to);
            request.AddParameter("subject", subject);
            request.AddParameter("template", "indygeneus");
            request.AddParameter("v:confirmationCode", _guid);

            request.Method = Method.POST;
            var stat = client.Execute(request);
            return stat;
        }
    }

}
