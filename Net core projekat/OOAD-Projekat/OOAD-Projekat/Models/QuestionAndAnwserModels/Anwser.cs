using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models
{
    public class Anwser
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool AcceptedAsAnwser { get; set; }
        [NotMapped]
        public IRating Rating { get; set; }
    }
}