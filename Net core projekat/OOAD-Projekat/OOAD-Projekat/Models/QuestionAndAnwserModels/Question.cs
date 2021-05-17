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
        public bool Double { get; set; }
        public bool Anwsered { get; set; }
        [NotMapped]
        public IRating Rating { get; set; }
        [NotMapped]
        public List<Tag> Tags{ get; set; }
        [NotMapped]
        public List<Question> Anwsers{ get; set; }

        public Question()
        {
            Tags = new List<Tag>();
            Anwsers = new List<Question>();
        }

        public void AddAnwser(Anwser anwser)
        {
            throw new NotImplementedException();
        }

        public void EditAnwser(Anwser anwser)
        {
            throw new NotImplementedException();
        }

        public void AddTag(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}