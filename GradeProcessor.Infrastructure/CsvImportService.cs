using CsvHelper;
using GradeProcessor.Domain.Entities;
using System.Drawing;
using System.Globalization;

namespace GradeProcessor.Infrastructure;

public class CsvImportService : IImport
{
    public List<LabSubmission> Import(string filePath, string moduleName, int penaltyWeek)
    {
        var submissions = new List<LabSubmission>();

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<dynamic>();

        foreach (var record in records)
        {
            var dict = (IDictionary<string, object>)record;

            string student = dict["Name"].ToString();

            var labScores = dict
                .Where(x => x.Key.StartsWith("Lab"))
                .Select(x => ParsePercentage(x.Value.ToString()))
                .ToList();

            submissions.Add(new LabSubmission
            {
                StudentName = student,
                ModuleName = moduleName,
                LabScores = labScores,
                PenaltyWeek = Enumerable.Repeat(penaltyWeek,labScores.Count).ToList()
            });
        }

        return submissions;
    }

    private double ParsePercentage(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0;

        if (double.TryParse(value.Trim().TrimEnd('%'), out double result))
            return result;

        return 0;
    }
}