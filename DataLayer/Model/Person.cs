using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class Person
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public string Firstname { get; set; }
        [MaxLength(100)]
        public string Othername { get; set; }
        [MaxLength(100)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public byte[] PhoneNo { get; set; }
        [MaxLength(50)]
        public byte[] Email { get; set; }
        public bool Active { get; set; }
    }
}
