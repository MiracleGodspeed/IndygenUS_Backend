using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Model
{
    public class SurveyQuestionOptions
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public SurveyQuestions SurveyQuestions { get; set; }
        public int SurveyQuestionsId { get; set; }
        public bool Active { get; set; }
    }
}
