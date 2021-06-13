using System;
using System.Collections.Generic;
using System.Linq;

namespace OOAD_Projekat.Models
{
    public class QuestionRating : IRating
    {

        private List<Reaction> reactions { get; set; }

        public Tuple<int, int, double> CalculateRating()
        {
            int numOfLikes = 0;

            reactions.ForEach(x => numOfLikes += (x.ReactionType == ReactionType.LIKE ? 1 : 0));

            var result = new Tuple<int, int, double>(numOfLikes, reactions.Count() - numOfLikes, 0);

            return result;
        }

        public void SetReactions(List<Reaction> reaction)
        {
            reactions = reaction;
        }
    }
}