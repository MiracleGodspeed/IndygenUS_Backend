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
                userDto.FullName = user.Person.Surname + " " + user.Person.Firstname + " " + user.Person.Othername;
                userDto.IsHOD = user.RoleId == 4 ? true : false;

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
                    Person person = new Person()
                    {
                        Surname = "-",
                        Firstname = loginDto.UserName,
                        Othername = "-",
                        Email = Utility.EncryptAesManaged(loginDto.UserName)
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
                        NationalityId = loginDto.NationalityId
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
        public async Task<int> ResetPassword(string Username)
        {
            try
            {
                var _username = Username.Trim();
                var getUser = await _context.USER.Where(x => x.Username == _username.Trim()).FirstOrDefaultAsync();
                if(getUser != null)
                {                   
                    string generateGuid = Convert.ToString(Guid.NewGuid());
                    var splitGuid = generateGuid.Split("-");
                    var guid = splitGuid[1];
                    getUser.Guid = guid;
                    _context.Update(getUser);
                    await _context.SaveChangesAsync();
                    return StatusCodes.Status200OK;
                }
                return 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        //public static IRestResponse SendSimpleMessage(string to, string body)
        //{
        //    string _body = "Your Verification code is : " + body;
        //    RestClient client = new RestClient();
        //    client.BaseUrl = new Uri("https://api.mailgun.net/v3/");
        //    client.Authenticator =
        //        new HttpBasicAuthenticator("api", "key-8540f3ef6a66cdaf8d9121f11c99aa6b");
        //    RestRequest request = new RestRequest();
        //    request.AddParameter("domain", "nrf.lloydant.com", ParameterType.UrlSegment);
        //    request.Resource = "{domain}/messages";
        //    request.AddParameter("from", "ABSU Elearn NG <mailgun@absuelearn.com>");
        //    request.AddParameter("to", to);
        //    //request.AddParameter("to", "YOU@YOUR_DOMAIN_NAME");
        //    request.AddParameter("subject", "Account Verification");
        //    request.AddParameter("text", _body);
        //    request.Method = Method.POST;
        //    var stat = client.Execute(request);
        //    return stat;
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

    }

}
