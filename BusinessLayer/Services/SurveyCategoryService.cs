using BusinessLayer.Interface;
using DataLayer.Dtos;
using DataLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class SurveyCategoryService : Repository<User>, ISurveyCategoryService
    {
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;
        private readonly string key;

        ResponseModel response = new ResponseModel();

        public SurveyCategoryService(DBContext context, IConfiguration configuration)
             : base(context)
        {
            // _context = context;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");
            key = _configuration.GetValue<string>("AppSettings:Key");


        }


        //public async Task<IEnumerable<SurveyCategoryListDto>> GetSurveyCategories()
        //{
        //    try
        //    {

        //        return await _context.SURVEY_CATEGORY.Where(x => x.Active)
        //            .Select(f => new SurveyCategoryListDto() { 
        //                SurveyCategoryId = f.Id,
        //                SurveyCategoryName = f.Name,
        //                ClassName = f.ClassName
        //            })
        //            .ToListAsync();
         

        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<IEnumerable<SurveyCategoryListDto>> GetSurveyCategories(string userId)
        {
            try
            {
                List<SurveyCategoryListDto> listDtos = new List<SurveyCategoryListDto>();
                var cat = await _context.SURVEY_CATEGORY.Where(x => x.Active).OrderBy(x => x.SortOrder).ToListAsync();
                if(cat != null && cat.Count > 0)
                {
                    foreach (var item in cat)
                    {

                        SurveyCategoryListDto dto = new SurveyCategoryListDto();

                        dto.SurveyCategoryName = item.Name;
                        dto.SurveyCategoryId = item.Id;
                        dto.ClassName = item.ClassName;


                        var subCat = await _context.SURVEY_SUB_CATEGORY.Where(x => x.Active && x.SurveyCategoryId == item.Id)
                            .Include(x => x.SurveyCategory)
                            .Select(f => new SubCategoryListDto()
                            {
                                SurveyCategoryId = f.SurveyCategoryId,
                                SubCategoryId = f.Id,
                                SubCategoryName = f.Name,
                                TimeAllowed = f.TimeAllowed,
                                Summary = f.Summary
                            })
                        .ToListAsync();
                        if(subCat != null && subCat.Count > 0)
                        {
                            List<SubCategoryListDto> subCategoryList = new List<SubCategoryListDto>();
                            foreach (SubCategoryListDto subCatObj in subCat)
                            {
                               
                                var hasQuestion = await _context.SURVEY_QUESTIONS.Where(x => x.Active && x.SurveySubCategoryId == subCatObj.SubCategoryId).ToListAsync();
                                var isAnswered = await _context.USER_SURVEY_RESPONSE.Where(x => x.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategoryId == subCatObj.SubCategoryId && x.UserId == userId)
                                    .Include(x => x.SurveyQuestionSubOptions)
                                    .ThenInclude(x => x.SurveyQuestionOptions)
                                    .ThenInclude(x => x.SurveyQuestions)
                                    .ThenInclude(x => x.SurveySubCategory)
                                    .ToListAsync();
                                if (hasQuestion != null && hasQuestion.Count > 0)
                                {
                                    subCatObj.HasQuestions = true;

                                }
                                else
                                {
                                    subCatObj.HasQuestions = false;

                                }
                                if (isAnswered != null && isAnswered.Count > 0)
                                {
                                    subCatObj.IsConducted = true;
                                }
                                else
                                {
                                    subCatObj.IsConducted = false;
                                }

                                subCategoryList.Add(subCatObj);
                            }
                            dto.SubCategoryList = subCategoryList;

                        }


                        // dto.SubCategoryList = subCat;


                        listDtos.Add(dto);
                    }

                }

                return listDtos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<SubCategoryListDto>> GetSubCategoryByCategoryId(int categoryId)
        {
            try
            {
                return await _context.SURVEY_SUB_CATEGORY.Where(x => x.Active && x.SurveyCategoryId == categoryId)
                    .Select(f => new SubCategoryListDto()
                    {
                        SurveyCategoryId = f.SurveyCategoryId,
                        SubCategoryId = f.Id,
                        SubCategoryName = f.Name,
                        TimeAllowed = f.TimeAllowed,
                        Summary = f.Summary
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<SurveyQuestionDto>> GetSurveyQuestionDetailsBySubCategory(long subCategoryId)
        {
            try
            {
                List<SurveyQuestionDto> finalSurveyList = new List<SurveyQuestionDto>();

                var surveyQuestions = await _context.SURVEY_QUESTIONS.Where(x => x.Active && x.SurveySubCategoryId == subCategoryId).ToListAsync();
                if(surveyQuestions != null && surveyQuestions.Count > 0)
                {
                    foreach(var questionStore in surveyQuestions)
                    {
                        SurveyQuestionDto questionDto = new SurveyQuestionDto();
                        QuestionOptionDto questionOptionDto = new QuestionOptionDto();
                        QuestionSubOptionDto questionSubOptionDto = new QuestionSubOptionDto();
                        List<QuestionOptionDto> questionOptionList = new List<QuestionOptionDto>();
                        List<QuestionSubOptionDto> questionSubOptionList = new List<QuestionSubOptionDto>();


                        questionDto.Question = questionStore.Name;
                        questionDto.SubCategoryId = questionStore.SurveySubCategoryId;
                        questionDto.InputType = questionStore.InputType;

                        //Get Survey Question Options using the question ID
                        var questionOptions = await _context.SURVEY_QUESTION_OPTIONS.Where(x => x.Active && x.SurveyQuestionsId == questionStore.Id).OrderBy(x => x.Name).ToListAsync();
                        if(questionOptions !=null && questionOptions.Count > 0)
                        {
                            foreach(var questionOptionStore in questionOptions)
                            {
                                QuestionOptionDto questionOption = new QuestionOptionDto();
                                questionOption.Name = questionOptionStore.Name;
                                questionOption.Id = questionOptionStore.Id;


                                //Get Survey Question Sub Options using the question option ID
                                var questionSubOptions = await _context.SURVEY_QUESTION_SUB_OPTIONS.Where(x => x.Active && x.SurveyQuestionOptionsId == questionOptionStore.Id)
                                    .Select(f => new QuestionSubOptionDto() { 
                                    Name = f.Name,
                                    Id = f.Id
                                    })
                                    .OrderBy(x => x.Name)
                                    .ToListAsync();
                                if (questionOptions != null && questionOptions.Count > 0)
                                {
                                    questionOption.QuestionSubOptions = questionSubOptions;
                                    questionOptionList.Add(questionOption);

                                }

                            }

                        }
                        questionOptionDto.QuestionSubOptions = questionSubOptionList;
                        questionDto.QuestionOptions = questionOptionList;
                        finalSurveyList.Add(questionDto);
                    }

                }
                return finalSurveyList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SubmitSurveyResponse(List<long> subOptionIds, string userId, long surveySubCategoryId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if(subOptionIds.Count > 0)
                {
                    var doesResponseExist = await _context.USER_SURVEY_RESPONSE.Where(x => x.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategoryId == surveySubCategoryId && x.UserId == userId)
                        .Include(x => x.SurveyQuestionSubOptions)
                        .ThenInclude(x => x.SurveyQuestionOptions)
                        .ThenInclude(x => x.SurveyQuestions)
                        .ToListAsync();

                    if (doesResponseExist != null && doesResponseExist.Count > 0)
                    {
                        foreach (var exist in doesResponseExist)
                        {
                            _context.Remove(exist);
                            await _context.SaveChangesAsync();

                        }
                    }
                    foreach (long item in subOptionIds)
                    {
                       // var doesResponseExist = await _context.USER_SURVEY_RESPONSE.Where(x => x.SurveyQuestionSubOptionsId == item && x.UserId == userId).FirstOrDefaultAsync();

                        UserSurveyResponse surveyResponse = new UserSurveyResponse()
                        {
                            SurveyQuestionSubOptionsId = item,
                            UserId = userId
                        };
                        _context.Add(surveyResponse);
                        await _context.SaveChangesAsync();
                    }

                    var isSubmitted = await _context.USER_SURVEY_SUBMISSION.Where(x => x.UserId == userId && x.SurveySubCategoryId == surveySubCategoryId).FirstOrDefaultAsync();
                    if(isSubmitted != null)
                    {
                        isSubmitted.DateSubmitted = DateTime.Now;
                        _context.Update(isSubmitted);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Submission submission = new Submission()
                        {
                            UserId = userId,
                            DateSubmitted = DateTime.Now,
                            SurveySubCategoryId = surveySubCategoryId
                        };
                        _context.Add(submission);
                        await _context.SaveChangesAsync();
                    }
                    
                }
                await transaction.CommitAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<UserCategoryReportDto>> GetUserReport(string userId)
        {
            try
            {
                var getCategories = await _context.USER_SURVEY_RESPONSE.Where(x => x.UserId == userId)
                    .Include(x => x.SurveyQuestionSubOptions)
                    .ThenInclude(x => x.SurveyQuestionOptions)
                    .ThenInclude(x => x.SurveyQuestions)
                    .ThenInclude(x =>x.SurveySubCategory)
                    .ThenInclude(x => x.SurveyCategory)
                    .Select(f => new UserCategoryReportDto() { 
                        CatrgoryId = f.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategory.SurveyCategory.Id,
                        CatrgoryName = f.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategory.SurveyCategory.Name
                    })
                    .ToListAsync();

                return RemoveDuplicatesRecords(getCategories);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


       
        public async Task<IEnumerable<UserReportLinks>> GetUserReportLinks(long surveyCategoryId, string userId)
        {
            try
            {
                List<UserReportLinks> resultLinks = new List<UserReportLinks>();
                var getCategories = await _context.USER_SURVEY_RESPONSE.Where(x => x.UserId == userId && x.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategory.SurveyCategory.Id == surveyCategoryId)
                   .Include(x => x.SurveyQuestionSubOptions)
                   .ThenInclude(x => x.SurveyQuestionOptions)
                   .ThenInclude(x => x.SurveyQuestions)
                   .ThenInclude(x => x.SurveySubCategory)
                   .ThenInclude(x => x.SurveyCategory)
                   .Select(f => new UserCategoryReportDto()
                   {
                       CatrgoryId = f.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategory.SurveyCategory.Id,
                       CatrgoryName = f.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategory.SurveyCategory.Name,
                       SurveyQuestionSubOptionId = f.SurveyQuestionSubOptions.Id
                   })
                   .ToListAsync();
                var filterList = RemoveDuplicatesRecords(getCategories);
                if(filterList != null && filterList.Count > 0)
                {
                    foreach(var item in getCategories)
                    {
                        var getLinks = await _context.SURVEY_SELECTION_RESULT_LINKS.Where(x => x.SurveyQuestionSubOptionsId == item.SurveyQuestionSubOptionId)
                            .Select(f => new UserReportLinks()
                            {
                                Link = f.VideoLink,
                                Title = f.Title,
                                PreviewText = f.PreviewText,
                                PreviewImage = f.PreviewImage,
                                QuestionSubOptionId = f.SurveyQuestionSubOptionsId
                            })
                            .ToListAsync();
                        resultLinks.AddRange(getLinks);
                    }
                }
                return resultLinks;
               // var reportLinks = await _context.SURVEY_SELECTION_RESULT_LINKS.Where()

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<BaseDto>> GetUserSurveyEntries(long surveySubCategoryId, string userId)
        {
            try
            {
                var getCategories = await _context.USER_SURVEY_RESPONSE.Where(x => x.UserId == userId && x.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategoryId == surveySubCategoryId)
                   .Include(x => x.SurveyQuestionSubOptions)
                   .ThenInclude(x => x.SurveyQuestionOptions)
                   .ThenInclude(x => x.SurveyQuestions)
                   .ThenInclude(x => x.SurveySubCategory)
                   .ThenInclude(x => x.SurveyCategory)
                   .Select(f => new BaseDto()
                   {
                       Id = f.SurveyQuestionSubOptionsId
                   })
                   .ToListAsync();

                return getCategories;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BaseDto>> GetUserSurveyOptionEntries(long surveySubCategoryId, string userId)
        {
            try
            {
                var getCategories = await _context.USER_SURVEY_RESPONSE.Where(x => x.UserId == userId && x.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategoryId == surveySubCategoryId)
                   .Include(x => x.SurveyQuestionSubOptions)
                   .ThenInclude(x => x.SurveyQuestionOptions)
                   .ThenInclude(x => x.SurveyQuestions)
                   .ThenInclude(x => x.SurveySubCategory)
                   .ThenInclude(x => x.SurveyCategory)
                   .Select(f => new BaseDto()
                   {
                       Id = f.SurveyQuestionSubOptions.SurveyQuestionOptions.Id
                   })
                   .ToListAsync();

                return RemoveDuplicateGeneral(getCategories);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateSurveyEntries(List<long> subOptionIds, string userId, long surveySubCategoryId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (subOptionIds.Count > 0)
                {
                    var doesResponseExist = await _context.USER_SURVEY_RESPONSE.Where(x => x.SurveyQuestionSubOptions.SurveyQuestionOptions.SurveyQuestions.SurveySubCategoryId == surveySubCategoryId && x.UserId == userId)
                        .Include(x => x.SurveyQuestionSubOptions)
                        .ThenInclude(x=> x.SurveyQuestionOptions)
                        .ThenInclude(x => x.SurveyQuestions)
                        .ToListAsync();

                    if(doesResponseExist != null && doesResponseExist.Count > 0)
                    {
                        foreach(var exist in doesResponseExist)
                        {
                            _context.Remove(exist);
                            await _context.SaveChangesAsync();

                        }
                    }

                    foreach (long item in subOptionIds)
                    {

                        UserSurveyResponse surveyResponse = new UserSurveyResponse()
                        {
                            SurveyQuestionSubOptionsId = item,
                            UserId = userId
                        };
                        _context.Add(surveyResponse);
                        await _context.SaveChangesAsync();
                    }

                    Submission submission = new Submission()
                    {
                        UserId = userId,
                        DateSubmitted = DateTime.Now,
                        SurveySubCategoryId = surveySubCategoryId
                    };
                    _context.Add(submission);
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UserCategoryReportDto> RemoveDuplicatesRecords(List<UserCategoryReportDto> items)
        {
            List<UserCategoryReportDto> returnList = new List<UserCategoryReportDto>();
            foreach (var item in items)
            {
                var isDuplicate = returnList.Where(x => x.CatrgoryId == item.CatrgoryId).ToList();
                if (isDuplicate == null || isDuplicate.Count <= 0)
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }

        public List<BaseDto> RemoveDuplicateGeneral(List<BaseDto> items)
        {
            List<BaseDto> returnList = new List<BaseDto>();
            foreach (var item in items)
            {
                var isDuplicate = returnList.Where(x => x.Id == item.Id).ToList();
                if (isDuplicate == null || isDuplicate.Count <= 0)
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }

        public async Task<IEnumerable<BaseDto>> GetAncestry()
        {
            return await _context.ANCESTRY_IDENTITY.Where(x => x.Active)
                .Select(f => new BaseDto
                {
                    Name = f.Ancestry,
                    Id = f.Id
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<BaseDto>> GetGender()
        {
            return await _context.GENDER.Where(x => x.Active)
                .Select(f => new BaseDto
                {
                    Name = f.Name,
                    Id = f.Id
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BaseDto>> GetASecurityQuestions()
        {
            return await _context.SECURITY_QUESTIONS.Where(x => x.Active)
                .Select(f => new BaseDto
                {
                    Name = f.Question,
                    Id = f.Id
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BaseDto>> GetSexualOrientation()
        {
            return await _context.SEXUAL_ORIENTATION.Where(x => x.Active)
                .Select(f => new BaseDto
                {
                    Name = f.Name,
                    Id = f.Id
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<BaseDto>> GetRegions()
        {
            return await _context.REGION.Where(x => x.Active)
                .Select(f => new BaseDto
                {
                    Name = f.Name,
                    Id = f.Id
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<BaseDto>> GetResponseType()
        {
            return await _context.RESPONSE_TYPE.Where(x => x.Active)
                .Select(f => new BaseDto
                {
                    Name = f.Response,
                    Id = f.Id
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BaseDto>> GetNationality()
        {
            return await _context.NATIONALITY.Where(x => x.Active)
                .Select(f => new BaseDto
                {
                    Name = f.Name,
                    Id = f.Id
                })
                .ToListAsync();
        }

       public async Task<int> PostSurveyQuestion(SurveyQuestionDto dto, string userId)
        {
            try
            {
                //var validateUserRole = ValidateAdmin(userId);
                //if (validateUserRole == null)
                //    throw new NullReferenceException("unauthorized");
                if (dto.SubCategoryId > 0)
                {
                    SurveyQuestions surveyQuestions = new SurveyQuestions()
                    {
                        Name = dto.Question,
                        SurveySubCategoryId = dto.SubCategoryId,
                        Active = true
                    };

                    _context.Add(surveyQuestions);
                    await _context.SaveChangesAsync();
                }
                return StatusCodes.Status200OK;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SurveyQuestionDto>> GetSurveyQuestions()
        {
            return await _context.SURVEY_QUESTIONS.Where(x => x.Active)
                .Include(x => x.SurveySubCategory)
                .Select(f => new SurveyQuestionDto{
                    Question = f.Name,
                    SubCategoryName = f.SurveySubCategory.Name
                })
                .ToListAsync();
        }

        public async Task<bool> ValidateAdmin(string userId)
        {
            var validateUserRole = await _context.USER.Where(x => x.Id == userId && x.Active && x.RoleId == (int)IndyKeys.AdminKey).FirstOrDefaultAsync();
            if (validateUserRole != null) return true;
            else return false;
        }

    }
}
