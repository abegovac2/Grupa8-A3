using System;

namespace OOAD_Projekat.Models
{
    public interface IRating
    {
        public Tuple<int, int> GetRatingForPost(int postId, PostTypeId postType);
        public void AddReaction(Reaction reaction);
    }
}