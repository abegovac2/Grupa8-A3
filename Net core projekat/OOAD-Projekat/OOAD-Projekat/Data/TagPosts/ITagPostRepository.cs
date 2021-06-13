using OOAD_Projekat.Models;
using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.TagPosts
{
    public interface ITagPostRepository
    {
        public Task AddTagPost(TagPost tagPost);
        public Task DeleteTagFromQuestion(Tag t, int questionID);
        public Task DeleteAllTagsFromQuestion(int questionID);
    }
}
