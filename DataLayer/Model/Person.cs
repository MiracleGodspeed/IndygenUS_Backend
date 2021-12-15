using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class Person
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public string Surname { get; set; }
        [MaxLength(50)]
        public string PhoneNo { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Active { get; set; }
    }
}
