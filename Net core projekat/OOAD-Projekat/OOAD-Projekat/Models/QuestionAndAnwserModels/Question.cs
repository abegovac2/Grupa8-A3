using OOAD_Projekat.Models.QuestionAndAnwserModels;
using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Anwsered { get; set; }
        public bool HotQuestion { get; set; }

        // One to Many relatinship with rating
        public ICollection<Rating> Ratings { get; set; }

        // Mark question as duplicate
        public bool Duplicate { get; set; }
        // Many to many relationship with tags
        public ICollection<TagPost> Tags { get; set; }
        // One to many relationship with answers
        public ICollection<Answer> Answers { get; set; }


        public void AddAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }

        public void Answer(Answer answer)
        {
            throw new NotImplementedException();
        }

        public void AddTag(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}