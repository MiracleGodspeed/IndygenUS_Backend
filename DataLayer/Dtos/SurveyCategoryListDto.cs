using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Dtos
{
    public class SurveyCategoryListDto
    {
        public int SurveyCategoryId { get; set; }
        public string SurveyCategoryName { get; set; }
        public string ClassName { get; set; }
        public List<SubCategoryListDto> SubCategoryList { get; set; }
    }


    public class SubCategoryListDto
    {
        public int SurveyCategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        //public string ClassName { get; set; }
        public string Summary { get; set; }
        public string TimeAllowed { get; set; }
        public bool HasQuestions { get; set; }
        public bool IsConducted { get; set; }
    }

    public class UserReportDto
    {
        public int CatrgoryId { get; set; }
        public long CatrgoryName { get; set; }
        public List<UserReportLinks> RelatedLinks { get; set; }
       
    }



    public class UserReportLinks
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string PreviewText { get; set; }
        public string PreviewImage { get; set; }
        public long QuestionSubOptionId { get; set; }

    }

    public class UserCategoryReportDto
    {
        public int CatrgoryId { get; set; }
        public long SurveyQuestionSubOptionId { get; set; }
        public string CatrgoryName { get; set; }

    }
}
