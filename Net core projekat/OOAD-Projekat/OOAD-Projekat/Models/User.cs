using Microsoft.AspNetCore.Identity;

namespace OOAD_Projekat.Models
{
    public class User : IdentityUser
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
    }
}