using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class AncestryIdentity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Ancestry { get; set; }
        public bool Active { get; set; }
    }
}
