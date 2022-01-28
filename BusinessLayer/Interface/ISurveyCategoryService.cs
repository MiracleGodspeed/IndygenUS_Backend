using DataLayer.Dtos;
using DataLayer.Model;
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
       Task<int> PostSurveyQuestion(SurveyQuestionDto dto, string userId);
       Task<IEnumerable<SurveyQuestionDto>> GetSurveyQuestions();
        Task<SurveyQuestionDto> GetSurveyQuestionDetailsBySubCategoryAndQuestionOrder(List<long> subOptionIds, long subCategoryId, long questionOrder, string userId, bool isFirst, bool isPrev);
      Task<IEnumerable<BaseDto>> GetUserSurveyEntriesAlt(long surveySubCategoryId, string userId, long questionId);
       Task<bool> AddSurveyQuestions(string question, long subCatId, bool active, int? layer, string inputType, long questionOrder);
       Task<bool> EditSurveyQuestions(string question, long subCatId, int? layer, string inputType, long questionOrder, long questionId);
       Task<bool> DeleteDeactivateSurveyQuestion(long questionId, bool isDelete, bool isActive);
       Task<bool> AddSurveyQuestionOptions(string name, int questionId, bool active, string inputType);
        Task<bool> EditSurveyQuestionOptions(long questionOptionId, SurveyQuestionOptions dto);
        Task<bool> DeleteDeactivateSurveyQuestionOption(long questionOptionId, bool isDelete, bool isActive);
        Task<bool> AddSurveySubOptions(SurveyQuestionSubOptions dto);
        Task<bool> EditSurveySubOptions(long subOptionId, SurveyQuestionSubOptionsPayload dto);
        Task<bool> DeleteDeactivateSurveySubOption(long subOptionId, bool isDelete, bool isActive);
        Task<IEnumerable<UserReportLinks>> GetEntryLinks(long subCategoryId);
        Task<bool> AddSurveyLinks(UserReportLinks dto);
        Task<bool> DeleteDeactivateSurveyResultLinks(long linkId, bool isDelete, bool isActive);
    }
}
