using System.ComponentModel.DataAnnotations;

namespace OOAD_Projekat.Models.ChatModels
{
    public class ChatUser
    {
        [Key]
        public string UserId { get; set; }
        public User User { get; set; }
        public int ChatId { get; set; }

        public Chat Chat { get; set; }
        public UserRole Role { get; set; }
    }
}
