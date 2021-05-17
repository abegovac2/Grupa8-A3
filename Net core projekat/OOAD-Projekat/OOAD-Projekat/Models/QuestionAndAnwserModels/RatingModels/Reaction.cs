using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models
{
    public class Reaction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public User LeftBy { get; set; }
        public DateTime TimeStamp { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}