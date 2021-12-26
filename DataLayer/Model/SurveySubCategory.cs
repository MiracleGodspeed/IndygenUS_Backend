using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Model
{
    public class SurveySubCategory
    {
        public long Id { get; set; }
        public SurveyCategory SurveyCategory { get; set; }
        public int SurveyCategoryId { get; set; }
        public string TimeAllowed { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public bool Active { get; set; }
    }
}
