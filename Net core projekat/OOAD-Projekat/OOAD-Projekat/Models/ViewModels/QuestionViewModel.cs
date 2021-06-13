using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOAD_Projekat.Models.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionViewModel()
        {
        }

        public QuestionViewModel(int id, string title, string content, string TagsString, List<Tag> tags, User user)
        {
            Id = id;
            Title = title;
            Content = content;
            Tags = TagsString;
            PopularTags = tags;
            this.user = user;
        }
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the title")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the content")]
        public string Content { get; set; }
        public string Tags { get; set; }
        public List<Tag> PopularTags { get; set; }
        public User user { get; set; }
    }
}
