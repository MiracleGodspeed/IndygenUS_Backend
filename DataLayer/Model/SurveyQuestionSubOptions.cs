using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class SurveyQuestionSubOptions
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public SurveyQuestionOptions SurveyQuestionOptions { get; set; }
        public int SurveyQuestionOptionsId { get; set; }
        public string InputType { get; set; }
        public long? SkipTo { get; set; }
        public bool Active { get; set; }
    }




    public class SurveyQuestionSubOptionsPayload
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int SurveyQuestionOptionsId { get; set; }
        public string InputType { get; set; }
        public long? SkipTo { get; set; }
        public bool Active { get; set; }
    }
}
