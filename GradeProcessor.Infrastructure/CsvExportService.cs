using GradeProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GradeProcessor.Infrastructure
{
    public class CsvExportService : IExport
    {
        public void Export(string filePath, Dictionary<string, Dictionary<string, ModuleGrade>> grades)
        {
            var modules = grades.Values.SelectMany(x => x.Keys).Distinct().OrderBy(x => x).ToList();

            using var writer = new StreamWriter(filePath);

            writer.WriteLine("Student Name," + string.Join(",", modules));

            foreach (var student in grades)
            {
                var studentNameParts = student.Key.Split(',');
                var studentName = studentNameParts.Length > 1 ? $"{studentNameParts[1].Trim()} {studentNameParts[0].Trim()}" : student.Key;
                var scores = student.Value.Select(x => Math.Round(x.Value.FinalScore).ToString());

                writer.WriteLine(studentName + "," + string.Join(",", scores));
            }
        }
    }
}
