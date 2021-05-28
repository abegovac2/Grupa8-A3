using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Poruka moze imati od 1-255 karaktera")]
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}