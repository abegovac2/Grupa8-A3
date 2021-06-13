using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.TagPosts
{
    public class TagPostRepository : ITagPostRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public TagPostRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task AddTagPost(TagPost tagPost)
        {
            var vecPostoji = applicationDbContext.TagPosts.Where(x => x.TagId == tagPost.TagId && x.QuestionId == tagPost.QuestionId).FirstOrDefault();
            if (vecPostoji == null)
            {
                applicationDbContext.TagPosts.Add(tagPost);
                await applicationDbContext.SaveChangesAsync();
            }

        }
        public async Task DeleteTagFromQuestion(Tag t, int questionID)
        {
            var pitanjeTag = await applicationDbContext.TagPosts.Where(tp => tp.TagId == t.Id && tp.QuestionId == questionID).FirstOrDefaultAsync();
            if (pitanjeTag != null)
            {
                applicationDbContext.TagPosts.Remove(pitanjeTag);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAllTagsFromQuestion(int questionID)
        {
            var tagovi = await applicationDbContext.TagPosts.Where(tp => tp.QuestionId == questionID).ToListAsync();
            if (tagovi != null)
            {
                tagovi.ForEach((t) =>
                {
                    applicationDbContext.TagPosts.Remove(t);
                });
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
