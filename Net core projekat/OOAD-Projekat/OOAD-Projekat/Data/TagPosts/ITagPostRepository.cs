using System.Threading.Tasks;
using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;

namespace OOAD_Projekat.Data.TagPosts
{
    public interface ITagPostRepository
    {
        public Task AddTagPost(TagPost tagPost);
    }
}
