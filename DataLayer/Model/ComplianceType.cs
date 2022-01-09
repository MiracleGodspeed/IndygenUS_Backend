﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class ComplianceType
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string ComplianceText { get; set; }
        public bool Active { get; set; }
    }
}
