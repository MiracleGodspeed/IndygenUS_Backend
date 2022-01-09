using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Model
{
    public class UserSurveyResponse
    {
        public long Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public SurveyQuestionSubOptions SurveyQuestionSubOptions { get; set; }
        public long SurveyQuestionSubOptionsId { get; set; }
    }
}
