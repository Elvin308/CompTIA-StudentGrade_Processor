using GradeProcessor.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GradeProcessor.Services
{
    public class GradeCalculatorService
    {
        private readonly PenaltyService _penaltyService;

        public GradeCalculatorService(PenaltyService penaltyService)
        {
            _penaltyService = penaltyService;
        }

        public void CompileLabSubmissions(ModuleGrade moduleGrade)
        {
            moduleGrade.ModuleName = moduleGrade.Submissions[0].ModuleName;
            moduleGrade.StudentName = moduleGrade.Submissions[0].StudentName;
            
            var compiledSubmissions = new LabSubmission
            {
                StudentName = moduleGrade.StudentName,
                ModuleName = moduleGrade.ModuleName,
                LabScores = new List<double>(),
                PenaltyWeek = new List<int>()
            };

            var bestPerLab = Enumerable.Range(0, moduleGrade.Submissions[0].LabScores.Count)
                .Select(i => moduleGrade.Submissions.Select(sub => new
                    {
                        RawScore = sub.LabScores[i],
                        PenaltyWeek = sub.PenaltyWeek[i],
                        Adjusted = _penaltyService.ApplyPenalty(sub.LabScores[i], sub.PenaltyWeek[i])
                    }).OrderByDescending(x => x.Adjusted).First()
                ).ToList();

            compiledSubmissions.LabScores = bestPerLab.Select(x => x.RawScore).ToList();
            compiledSubmissions.PenaltyWeek = bestPerLab.Select(x => x.PenaltyWeek).ToList();

            moduleGrade.FinalCompiled = compiledSubmissions;
            moduleGrade.FinalScore = bestPerLab.Select(x => x.Adjusted).Average();
        }
    }
}
