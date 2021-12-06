using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class ResponseType
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Response { get; set; }
        public bool Active { get; set; }
    }
}
