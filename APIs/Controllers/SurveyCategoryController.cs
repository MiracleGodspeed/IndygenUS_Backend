using BusinessLayer.Interface;
using DataLayer.Dtos;
using DataLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyCategoryController : ControllerBase
    {

        private readonly ISurveyCategoryService _service;
        private readonly IConfiguration _configuration;
        private readonly string key;
        public SurveyCategoryController(ISurveyCategoryService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
            key = _configuration.GetValue<string>("AppSettings:Key");

        }
        [HttpGet("SurveyCategories")]
        public async Task<IEnumerable<SurveyCategoryListDto>> GetSurveyCategories(string userId) => await _service.GetSurveyCategories(userId);
        //[Authorize]
        [HttpGet("FetchSubCategoryByCategory")]
        public async Task<IEnumerable<SubCategoryListDto>> GetSubCategoryByCategoryId(int categoryId) => await _service.GetSubCategoryByCategoryId(categoryId);
        [HttpGet("SurveyExerciseConditionalLogic")]
        public async Task<IEnumerable<SurveyQuestionDto>> GetSurveyQuestionDetailsBySubCategory(long subCategoryId) => await _service.GetSurveyQuestionDetailsBySubCategory(subCategoryId);
        [HttpPost("PostSurveyResponse")]
        public async Task<bool> SubmitSurveyResponse(List<long> subOptionIds, string userId, long surveySubCategoryId) => await _service.SubmitSurveyResponse(subOptionIds, userId, surveySubCategoryId);
        [HttpGet("UserReportCategory")]
        public async Task<IEnumerable<UserCategoryReportDto>> GetUserReport(string userId) => await _service.GetUserReport(userId);
        [HttpGet("UserReporyByCategory")]
        public async Task<IEnumerable<UserReportLinks>> GetUserReportLinks(long surveyCategoryId, string userId) => await _service.GetUserReportLinks(surveyCategoryId, userId);
        [HttpGet("[action]")]
        public async Task<IEnumerable<BaseDto>> GetAncestry() => await _service.GetAncestry();
        [HttpGet("[action]")]
        public async Task<IEnumerable<BaseDto>> GetGender() => await _service.GetGender();
        [HttpGet("[action]")]
        public async Task<IEnumerable<BaseDto>> GetASecurityQuestions() => await _service.GetASecurityQuestions();
        [HttpGet("[action]")]
        public async Task<IEnumerable<BaseDto>> GetSexualOrientation() => await _service.GetSexualOrientation();
        [HttpGet("[action]")]
        public async Task<IEnumerable<BaseDto>> GetResponseType() => await _service.GetResponseType();
        [HttpGet("[action]")]
        public async Task<IEnumerable<BaseDto>> GetNationality() => await _service.GetNationality();
        [HttpGet("[action]")]
        public async Task<IEnumerable<BaseDto>> GetUserSurveyEntries(long surveySubCategoryId, string userId) => await _service.GetUserSurveyEntries(surveySubCategoryId, userId);
        [HttpGet("[action]")]
        public async Task<IEnumerable<BaseDto>> GetRegions() => await _service.GetRegions();
        [HttpGet("UserEntryCategories")]
        public async Task<IEnumerable<BaseDto>> GetUserSurveyOptionEntries(long surveySubCategoryId, string userId) => await _service.GetUserSurveyOptionEntries(surveySubCategoryId, userId);
        [HttpPost("NewSurveyQuestion")]
        public async Task<int> PostSurveyQuestion(SurveyQuestionDto dto, string userId) => await _service.PostSurveyQuestion(dto, userId);
        [HttpGet("FetchSurveyQuestions")]
        public async Task<IEnumerable<SurveyQuestionDto>> GetSurveyQuestions() => await _service.GetSurveyQuestions();
        [HttpPost("[action]")]
        public async Task<SurveyQuestionDto> GetSurveyQuestionDetailsBySubCategoryAndQuestionOrder(List<long> subOptionIds, long subCategoryId, long questionOrder, string userId, bool isFirst, bool isPrev) => await _service.GetSurveyQuestionDetailsBySubCategoryAndQuestionOrder(subOptionIds, subCategoryId, questionOrder, userId, isFirst, isPrev);
        [HttpGet("[action]")]
        public async Task<IEnumerable<BaseDto>> GetUserSurveyEntriesAlt(long surveySubCategoryId, string userId, long questionId) => await _service.GetUserSurveyEntriesAlt(surveySubCategoryId, userId, questionId);
        [HttpPost("[action]")]
        public async Task<bool> AddSurveyQuestions(string question, long subCatId, bool active, int? layer, string inputType, long questionOrder) => await _service.AddSurveyQuestions(question, subCatId, active, layer, inputType, questionOrder);
        [HttpPost("[action]")]
        public async Task<bool> EditSurveyQuestions(string question, long subCatId, int? layer, string inputType, long questionOrder, long questionId) => await _service.EditSurveyQuestions(question, subCatId, layer, inputType, questionOrder, questionId);
        [HttpPost("[action]")]
        public async Task<bool> DeleteDeactivateSurveyQuestion(long questionId, bool isDelete, bool isActive) => await _service.DeleteDeactivateSurveyQuestion(questionId, isDelete, isActive);
        [HttpPost("[action]")]
        public async Task<bool> AddSurveyQuestionOptions(string name, int questionId, bool active, string inputType) => await _service.AddSurveyQuestionOptions(name, questionId, active, inputType);
        [HttpPost("[action]")]
        public async Task<bool> EditSurveyQuestionOptions(long questionOptionId, SurveyQuestionOptions dto) => await _service.EditSurveyQuestionOptions(questionOptionId, dto);
        [HttpPost("[action]")]
        public async Task<bool> DeleteDeactivateSurveyQuestionOption(long questionOptionId, bool isDelete, bool isActive) => await _service.DeleteDeactivateSurveyQuestionOption(questionOptionId, isDelete, isActive);
        [HttpPost("[action]")]
        public async Task<bool> AddSurveySubOptions(SurveyQuestionSubOptions dto) => await _service.AddSurveySubOptions(dto);
        [HttpPost("[action]")]
        public async Task<bool> EditSurveySubOptions(long subOptionId, SurveyQuestionSubOptionsPayload dto) => await _service.EditSurveySubOptions(subOptionId, dto);
        [HttpPost("[action]")]
        public async Task<bool> DeleteDeactivateSurveySubOption(long subOptionId, bool isDelete, bool isActive) => await _service.DeleteDeactivateSurveySubOption(subOptionId, isDelete, isActive);
        [HttpGet("[action]")]
        public async Task<IEnumerable<UserReportLinks>> GetEntryLinks(long subCategoryId) => await _service.GetEntryLinks(subCategoryId);
        [HttpPost("[action]")]
        public async Task<bool> AddSurveyLinks(UserReportLinks dto) => await _service.AddSurveyLinks(dto);
        [HttpPost("[action]")]
        public async Task<bool> DeleteDeactivateSurveyResultLinks(long linkId, bool isDelete, bool isActive) => await _service.DeleteDeactivateSurveyResultLinks(linkId, isDelete, isActive);
    }
}
