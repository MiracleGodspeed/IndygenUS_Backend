using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class SecurityQuestion
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Question { get; set; }
        public bool Active { get; set; }
    }
}
