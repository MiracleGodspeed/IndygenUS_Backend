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
        //Task<long> PostUser(AddUserDto userDto);
        //Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);
        //Task<GetUserProfileDto> GetUserProfile(long userId);
        //Task<ResponseModel> ProfileUpdate(UpdateUserProfileDto dto);
        //Task<IEnumerable<GetInstitutionUsersDto>> GetAllStudents();
    }
}
