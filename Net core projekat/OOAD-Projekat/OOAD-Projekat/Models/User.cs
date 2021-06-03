using Microsoft.AspNetCore.Identity;
using OOAD_Projekat.Models.ChatModels;
using OOAD_Projekat.Models.QuestionAndAnwserModels;
using System.Collections.Generic;

namespace OOAD_Projekat.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string Picture { get; set; }

        public bool Blocked { get; set; }
        public ICollection<ChatUser> Chats { get; set; }
        public ICollection<ViewedQuestionsHistory> ViewedQuestionsHistory { get; set; }

        // TODO: Omogucit editovanje korisnickih podataka 
    }
}