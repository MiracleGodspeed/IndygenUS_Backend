using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class SurveyQuestions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SurveySubCategory SurveySubCategory { get; set; }
        public long SurveySubCategoryId { get; set; }
        public bool Active { get; set; }
    }
}
