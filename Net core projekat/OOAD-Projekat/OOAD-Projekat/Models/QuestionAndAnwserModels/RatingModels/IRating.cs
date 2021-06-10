using System;
using System.Collections.Generic;

namespace OOAD_Projekat.Models
{
    public interface IRating
    {
        public Tuple<int, int, double> CalculateRating();
        public void SetReactions(List<Reaction> reaction);
    }
}