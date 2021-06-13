using OOAD_Projekat.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.ReactionData
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly ApplicationDbContext _context;

        public ReactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /*private async Task<bool> AllreadyReacted(string UserId, int PostId, PostType postType)
        {
            var reaction = await _context.Reactions
                .Where(x => x.UserId == UserId && x.PostId == PostId && x.PostType == postType)
                .FirstOrDefaultAsync();

            return (reaction != null);
        }*/

        private Reaction GetReaction(string UserId, int PostId, PostType postType)
        {
            var reaction = _context.Reactions
                .Where(x => x.UserId == UserId && x.PostId == PostId && x.PostType == postType)
                .FirstOrDefault();

            return reaction;
        }

        public async Task AddReactionFromPost(string UserId, int PostId, PostType postType, ReactionType reactionType)
        {
            var reaction = GetReaction(UserId, PostId, postType);
            if (reaction == null)
            {
                reaction = new Reaction
                {
                    UserId = UserId,
                    PostId = PostId,
                    PostType = postType,
                    ReactionType = reactionType
                };

                _context.Reactions.Add(reaction);
            }
            else
            {
                if (reaction.ReactionType == reactionType) return;
                reaction.ReactionType = reactionType;
                _context.Reactions.Update(reaction);
            }
            await _context.SaveChangesAsync();

        }

        /*private async Task<bool> CheckIfReactedSame(string UserId, int PostId, PostType postType, ReactionType reactionType)
        {
            var reaction = await _context.Reactions
                .Where(x => x.UserId == UserId && x.PostId == PostId && x.PostType == postType && x.ReactionType == reactionType)
                .FirstOrDefaultAsync();

            return (reaction != null);
        }*/

        public List<Reaction> GetReactionsForPost(int PostId, PostType postType)
        {
            var result = _context.Reactions
                .Where(x => x.PostId == PostId && x.PostType == postType)
                .ToList();
            return result;
        }

        public List<Reaction> GetReactionsForUser(string UserId)
        {
            var result = _context.Reactions
                .Where(x => x.UserId == UserId)
                .ToList();
            return result;
        }

        public async Task DeleteReactionsForPost(int PostId, PostType postType)
        {
            var reactions = GetReactionsForPost(PostId, postType);

            reactions.ForEach(
                x =>
                {
                    _context.Reactions.Remove(x);
                });

            await _context.SaveChangesAsync();

        }
    }
}
