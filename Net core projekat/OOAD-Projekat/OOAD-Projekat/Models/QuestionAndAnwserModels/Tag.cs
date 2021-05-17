using System.ComponentModel.DataAnnotations;

namespace OOAD_Projekat.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string TagContent { get; set; }
        public int NumOfUses { get; set; }
    }
}