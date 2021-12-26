using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Dtos
{
    public class SurveyQuestionDto
    {
        public string Question { get; set; }
        public long SubCategoryId { get; set; }
        public List<QuestionOptionDto> QuestionOptions { get; set; }
    }
    public class QuestionOptionDto
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public List<QuestionSubOptionDto> QuestionSubOptions { get; set; }

    }

    public class QuestionSubOptionDto
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}
