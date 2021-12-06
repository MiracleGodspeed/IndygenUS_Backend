using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Model
{
    public class DBInitializer
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DBContext(serviceProvider.GetRequiredService<DbContextOptions<DBContext>>());
            context.Database.EnsureCreated();


            //    var resp = new ResponseType[]
            //      {
            //            new ResponseType{ Active = true, Response = "Yes"},
            //            new ResponseType{ Active = true, Response = "No"},
            //            new ResponseType{ Active = true, Response = "Maybe"},


            //      };
            //    foreach (ResponseType res in resp)
            //    {
            //        context.Add(res);
            //    }
            //    await context.SaveChangesAsync();


            //    var surv = new SurveyCategory[]
            //     {
            //            new SurveyCategory{ Active = true, Name = "Personal Health", ClassName="personal-health"},
            //            new SurveyCategory{ Active = true, Name = "Family Health", ClassName="fam-health"},
            //            new SurveyCategory{ Active = true, Name = "#fortheculture", ClassName="forthe"},
            //            new SurveyCategory{ Active = true, Name = "Covid19", ClassName="covid"},
            //            new SurveyCategory{ Active = true, Name = "Genetics", ClassName="genetics"},



            //     };
            //    foreach (SurveyCategory res in surv)
            //    {
            //        context.Add(res);
            //    }
            //    await context.SaveChangesAsync();




            //    var role = new Role[]
            //      {
            //            new Role{ Active = true, Name = "User"},           

            //      };
            //    foreach (Role res in role)
            //    {
            //        context.Add(res);
            //    }
            //    await context.SaveChangesAsync();


            //    var nat = new Nationality[]
            //      {
            //            new Nationality{ Active = true, Name = "Nigeria"},

            //      };
            //    foreach (Nationality res in nat)
            //    {
            //        context.Add(res);
            //    }
            //    await context.SaveChangesAsync();

            //    var platform = new PlatformDiscoveryType[]
            //      {
            //            new PlatformDiscoveryType{ Active = true, Name = "Social Media"},
            //            new PlatformDiscoveryType{ Active = true, Name = "Byusforall Website"},
            //            new PlatformDiscoveryType{ Active = true, Name = "IndyGeneUS Health Website"},
            //            new PlatformDiscoveryType{ Active = true, Name = "Referal"},


            //      };
            //    foreach (PlatformDiscoveryType pt in platform)
            //    {
            //        context.Add(pt);
            //    }
            //    await context.SaveChangesAsync();







            //}

        }

    }
}
