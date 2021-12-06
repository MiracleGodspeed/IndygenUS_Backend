using DataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ISurveyCategoryService
    {
        Task<IEnumerable<SurveyCategoryListDto>> GetSurveyCategories(string userId);
        Task<IEnumerable<SubCategoryListDto>> GetSubCategoryByCategoryId(int categoryId);
        Task<IEnumerable<SurveyQuestionDto>> GetSurveyQuestionDetailsBySubCategory(long subCategoryId);
        Task<bool> SubmitSurveyResponse(List<long> subOptionIds, string userId, long surveySubCategoryId);
        Task<IEnumerable<UserCategoryReportDto>> GetUserReport(string userId);
        Task<IEnumerable<UserReportLinks>> GetUserReportLinks(long surveyCategoryId, string userId);
    }
}
