using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models
{
    public class NotifyUser
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int PostId { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}