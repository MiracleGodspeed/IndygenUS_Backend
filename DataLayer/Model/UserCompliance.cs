using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Model
{
    public class UserCompliance
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public ComplianceType ComplianceType { get; set; }
        public int ComplianceTypeId { get; set; }
        public DateTime DateEntered { get; set; }
        public bool Active { get; set; }
    }
}
