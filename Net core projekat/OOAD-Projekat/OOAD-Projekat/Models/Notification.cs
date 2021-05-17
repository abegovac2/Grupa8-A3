using System.ComponentModel.DataAnnotations;

namespace OOAD_Projekat.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public bool Read { get; set; }
    }
}