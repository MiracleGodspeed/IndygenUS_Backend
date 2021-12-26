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
        Task<IEnumerable<BaseDto>> GetAncestry();
        Task<IEnumerable<BaseDto>> GetGender();
        Task<IEnumerable<BaseDto>> GetASecurityQuestions();
        Task<IEnumerable<BaseDto>> GetSexualOrientation();
        Task<IEnumerable<BaseDto>> GetResponseType();
        Task<IEnumerable<BaseDto>> GetNationality();
        Task<IEnumerable<BaseDto>> GetUserSurveyEntries(long surveySubCategoryId, string userId);
        Task<IEnumerable<BaseDto>> GetRegions();
       Task<IEnumerable<BaseDto>> GetUserSurveyOptionEntries(long surveySubCategoryId, string userId);
    }
}
