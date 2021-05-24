using Microsoft.AspNetCore.Identity;

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

        // TODO: Omogucit editovanje korisnickih podataka 
    }
}