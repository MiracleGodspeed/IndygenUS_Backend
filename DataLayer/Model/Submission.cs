using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Model
{
    public class Submission
    {
        public long Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public SurveySubCategory SurveySubCategory { get; set; }
        public long SurveySubCategoryId { get; set; }
        //public bool Active { get; set; }
    }
}
