using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Dtos
{
    public class SurveyQuestionDto
    {
        public string Question { get; set; }
        public long SubCategoryId { get; set; }
        public long QuestionOrder { get; set; }
        public string SubCategoryName { get; set; }
        public string InputType { get; set; }
        public int? Layer { get; set; }
        public long Id { get; set; }
        public List<QuestionOptionDto> QuestionOptions { get; set; }
    }
    public class QuestionOptionDto
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public string InputType { get; set; }
        public List<QuestionSubOptionDto> QuestionSubOptions { get; set; }

    }

    public class QuestionSubOptionDto
    {
        public string Name { get; set; }
        public string InputType { get; set; }
        public long Id { get; set; }
        public bool IsAnswered { get; set; }
        public long? SkipTo { get; set; }

    }

    public class QuestionOptionDtoAlt
    {
        public string Name { get; set; }
        public string InputType { get; set; }
        public string SuboptionsSplit { get; set; }

    }
    //public class QuestionSubOptionDtoAlt
    //{
    //    public string Name { get; set; }
    //}
}
