using System;

namespace OOAD_Projekat.Models
{
    public interface IRating
    {
        public Tuple<int, int> GetRatingForPost(int postId, PostType postType);
        public void AddReaction(Reaction reaction);
    }
}