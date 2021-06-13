namespace OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels
{
    public class TagPost
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }
}
