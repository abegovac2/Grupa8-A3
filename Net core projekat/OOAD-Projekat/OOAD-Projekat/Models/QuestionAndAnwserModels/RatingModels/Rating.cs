using System;

namespace OOAD_Projekat.Models
{
    public class Rating : IRating{
        public int PostId { get; set; }
        public PostType PostType{ get; set; }

        public void AddReaction(Reaction reaction)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> GetRatingForPost(int postId, PostType postType)
        {
            throw new NotImplementedException();
        }

        public Rating(int postId, PostType postType)
        {
            PostId = postId;
            PostType = postType;
        }
    }
}