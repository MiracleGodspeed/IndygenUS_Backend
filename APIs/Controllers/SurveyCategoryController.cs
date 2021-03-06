using BusinessLayer.Interface;
using DataLayer.Dtos;
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
    }
}
