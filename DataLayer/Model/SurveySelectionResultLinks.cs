using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Model
{
    public class SurveySelectionResultLinks
    {
        public long Id { get; set; }
        public SurveyQuestionSubOptions SurveyQuestionSubOptions { get; set; }
        public long SurveyQuestionSubOptionsId { get; set; }
        public string VideoLink { get; set; }
        public string Title { get; set; }
        public string PreviewText { get; set; }
        public string PreviewImage { get; set; }
        public bool Active { get; set; }
    }
}
