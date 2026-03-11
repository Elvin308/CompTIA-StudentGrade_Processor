using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProcessor.Services
{
    public class PenaltyService
    {
        private readonly Dictionary<int, double> _penalties;

        public PenaltyService(Dictionary<int, double> penalties)
        {
            _penalties = penalties;
        }

        public double ApplyPenalty(double score, int penaltyWeek)
        {
            if (_penalties.ContainsKey(penaltyWeek))
            {
                return score - _penalties[penaltyWeek];
            }

            return score;
        }
    }
}
