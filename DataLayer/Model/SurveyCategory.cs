using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class SurveyCategory
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string ClassName { get; set; }
        public long? SortOrder { get; set; }
        public bool Active { get; set; }
    }
}
