using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Chat")]
        public string UserId { get; set; }
        public int PostId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
    }
}