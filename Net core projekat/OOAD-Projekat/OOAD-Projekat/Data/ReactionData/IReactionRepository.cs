using OOAD_Projekat.Models;
using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.ReactionData
{
    public interface IReactionRepository
    {
        public List<Reaction> GetReactionsForPost(int PostId, PostType postType);
        public List<Reaction> GetReactionsForUser(string UserId);
        public Task AddReactionFromPost(string UserId, int PostId, PostType postType, ReactionType reactionType);
        public Task DeleteReactionsForPost(int PostId, PostType postType);
    }
}
