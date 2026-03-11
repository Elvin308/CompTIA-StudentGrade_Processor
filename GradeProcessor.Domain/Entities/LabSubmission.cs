using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProcessor.Domain.Entities
{
    public class LabSubmission
    {
        public string StudentName { get; set; }
        public string ModuleName { get; set; }
        public List<double> LabScores { get; set; } = new();
        public List<int> PenaltyWeek { get; set; } = new();
    }
}
