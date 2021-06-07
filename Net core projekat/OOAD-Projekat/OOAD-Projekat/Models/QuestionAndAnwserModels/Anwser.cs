using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool AcceptedAsAnwser { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public int QuestionID { get; set; }
        public Question Question { get; set; }

        [NotMapped]
        public IRating ratingCalculate { get; set; }

        public float getAnwserRating()
        {
            //return ratingCalculate;
            throw new NotImplementedException();
        }
    }
}