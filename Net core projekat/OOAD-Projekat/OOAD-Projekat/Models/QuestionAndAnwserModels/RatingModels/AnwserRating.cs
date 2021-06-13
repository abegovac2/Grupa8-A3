using System;
using System.Collections.Generic;

namespace OOAD_Projekat.Models
{
    public class AnwserRating : IRating
    {
        private List<Reaction> reactions { get; set; }

        public Tuple<int, int, double> CalculateRating()
        {
            int numOfLikes = 0;

            reactions.ForEach(x => numOfLikes += (x.ReactionType == ReactionType.LIKE ? 1 : 0));

            double res = ((int)((numOfLikes / (double)(reactions.Count == 0 ? 1 : reactions.Count)) * 50)) / 10.0;

            return new Tuple<int, int, double>(-1, -1, res);
        }

        public void SetReactions(List<Reaction> reaction)
        {
            reactions = reaction;
        }
    }
}