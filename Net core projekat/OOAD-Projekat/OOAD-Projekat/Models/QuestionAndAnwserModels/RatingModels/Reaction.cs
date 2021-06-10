using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models
{
    public class Reaction
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public PostType PostType { get; set; }
        public string UserId{ get; set; }
        public DateTime TimeStamp { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}