using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOAD_Projekat.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string TagContent { get; set; }
        public int NumOfUses { get; set; }
        public ICollection<TagPost> TagPosts { get; set; }
    }
}