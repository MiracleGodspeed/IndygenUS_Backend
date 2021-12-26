using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Model
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(f => f.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelbuilder);
        }

        public DbSet<Person> PERSON { get; set; }
        public DbSet<User> USER { get; set; }
        public DbSet<SecurityQuestion> SECURITY_QUESTIONS { get; set; }
        public DbSet<ResponseType> RESPONSE_TYPE { get; set; }
        public DbSet<AncestryIdentity> ANCESTRY_IDENTITY { get; set; }
        public DbSet<Nationality> NATIONALITY { get; set; }
        public DbSet<PlatformDiscoveryType> PLATFORM_DISCOVERY_TYPE { get; set; }
        public DbSet<UserFringeDetails> USER_FRINGE_DETAILS { get; set; }
        public DbSet<SurveyCategory> SURVEY_CATEGORY { get; set; }
        public DbSet<SurveyQuestions> SURVEY_QUESTIONS { get; set; }
        public DbSet<SurveyQuestionOptions> SURVEY_QUESTION_OPTIONS { get; set; }
        public DbSet<SurveyQuestionSubOptions> SURVEY_QUESTION_SUB_OPTIONS { get; set; }
        public DbSet<SurveySelectionResultLinks> SURVEY_SELECTION_RESULT_LINKS { get; set; }
        public DbSet<Submission> USER_SURVEY_SUBMISSION { get; set; }
        public DbSet<UserSurveyResponse> USER_SURVEY_RESPONSE { get; set; }
        public DbSet<UserSecurityQuestions> USER_SECURITY_QUESTIONS { get; set; }
        public DbSet<SurveySubCategory> SURVEY_SUB_CATEGORY { get; set; }
        public DbSet<Gender> GENDER { get; set; }
        public DbSet<Role> ROLE { get; set; }
        public DbSet<ComplianceType> COMPLIANCE_TYPE { get; set; }
        public DbSet<UserCompliance> USER_COMPLIANCE { get; set; }
        public DbSet<SexualOrientation> SEXUAL_ORIENTATION { get; set; }
        public DbSet<Region> REGION { get; set; }



    }
}
