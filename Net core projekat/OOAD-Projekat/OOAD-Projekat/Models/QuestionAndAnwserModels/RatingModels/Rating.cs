using System;
using System.ComponentModel.DataAnnotations;

namespace OOAD_Projekat.Models
{
    public class Rating : IRating
    {
        [Key]
        public int id { get; set; }
        public int PostId { get; set; }
        public PostTypeId PostTypeId { get; set; }
        public PostTypeId PostType { get; set; }

        // One to many relationship with answers
        public Answer Answer { get; set; }
        public void AddReaction(Reaction reaction)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> GetRatingForPost(int postId, PostTypeId postType)
        {
            throw new NotImplementedException();
        }
    }
}