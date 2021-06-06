using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using System;
using System.Collections.Generic;
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
            applicationDbContext.TagPosts.Add(tagPost);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
