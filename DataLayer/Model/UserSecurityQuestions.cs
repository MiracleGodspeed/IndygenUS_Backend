using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Model
{
    public class UserSecurityQuestions
    {
        public long Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public SecurityQuestion SecurityQuestion { get; set; }
        public int SecurityQuestionId { get; set; }
       public string SecurityAnswer { get; set; }
        public bool Active { get; set; }


    }
}
