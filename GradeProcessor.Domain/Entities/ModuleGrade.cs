using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProcessor.Domain.Entities
{
    public class ModuleGrade
    {
        public string StudentName { get; set; }
        public string ModuleName { get; set; }
        public List<LabSubmission> Submissions { get; set; } = new();
        public LabSubmission FinalCompiled { get; set; }
        public double FinalScore { get; set; }
    }
}
