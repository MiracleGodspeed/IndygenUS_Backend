using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public long PersonId { get; set; }
        public long? RegionId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public bool SecurityQuestion { get; set; }
        public bool IsVerified { get; set; }
        public bool IsUpdatedProfile { get; set; }
        public bool IsHOD { get; set; }
    }

    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int NationalityId { get; set; }
        public long RegionId { get; set; }
    }
}
