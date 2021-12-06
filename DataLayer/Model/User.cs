using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class User : BaseModel
    {
        [MaxLength(50)]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsVerified { get; set; }
        public bool IsAnsweredSecurityQuestion { get; set; }
        public bool IsUpdatedProfile { get; set; }
        public string Guid { get; set; }
        public DateTime SignUpDate { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public Role Role { get; set; }
        public long RoleId { get; set; }
        public bool Active { get; set; }
    }

    public class AppSettings
    {
        public string Key { get; set; }
    }
}
