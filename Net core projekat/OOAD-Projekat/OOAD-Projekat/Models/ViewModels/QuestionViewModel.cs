using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Models.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the title")]
         public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the content")]
        public string Content { get; set; }
        public string Tags { get; set; }
    }
}
