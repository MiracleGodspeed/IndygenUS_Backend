using DataLayer.Dtos;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserService : IRepository<User>
    {

        Task<UserDto> AuthenticateUser(LoginDto dto, string injectkey);
        Task<UserDto> NewUser(RegisterDto loginDto);
        Task<int> UpdateUserProfile(UpdateProfileDto dto, string userId);
        Task<UpdateProfileDto> GetUserProfile(string userId);
        Task<int> PostSecurityQuestions(string userId, List<BaseDto> dto);
        Task<int> PostUserAgreement(string userId, int agreementType);
        Task<IEnumerable<BaseDto>> GetUserComplianceType(string userId);
        Task<int> ResetPassword(string email);
        Task<int> ConfirmResetPassword(string email, string code, string password);
        Task<int> ChangePassword(ChangePasswordDto dto);


    }
}
