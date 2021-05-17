using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models.ChatModels
{
    public class ChatUser
    {
        [Key]
        public string UserId { get; set; }
        [NotMapped]
        public User User { get; set; }
        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        [NotMapped]
        public Chat Chat { get; set; }
        public UserRole Role { get; set; }
    }
}
