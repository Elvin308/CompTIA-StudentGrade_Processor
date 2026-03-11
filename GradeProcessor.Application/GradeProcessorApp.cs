using GradeProcessor.Domain.Entities;
using GradeProcessor.Infrastructure;
using GradeProcessor.Services;

namespace GradeProcessor.Application
{
    public class GradeProcessorApp
    {
        private readonly IImport _importService;
        private readonly IExport _exportService;
        private readonly GradeCalculatorService _gradeService;
        private readonly Dictionary<string, int> _penaltyFiles;

        public GradeProcessorApp(IImport importService,IExport exportService,GradeCalculatorService gradeService,Dictionary<string, int> penaltyFiles)
        {
            _importService = importService;
            _exportService = exportService;
            _gradeService = gradeService;
            _penaltyFiles = penaltyFiles;
        }

        public void MainProgram(string rootFolder)
        {
            
            // Key: StudentName, Value: Dictionary<ModuleName, ModuleGrade()>
            var finalGrades = new Dictionary<string, Dictionary<string, ModuleGrade>>();

            // Process each module folder
            foreach (var moduleDir in Directory.GetDirectories(rootFolder))
            {
                string moduleName = Path.GetFileName(moduleDir);

                // Look for CSV files in the module directory Ex: before_due, week1, week2, week3
                foreach (var file in Directory.GetFiles(moduleDir, "*.csv"))
                {
                    string name = Path.GetFileNameWithoutExtension(file).ToLower();

                    if (!_penaltyFiles.ContainsKey(name))
                        continue;

                    var submissions = _importService.Import(file, moduleName, _penaltyFiles[name]);

                    foreach (var sub in submissions)
                    {

                        if (!finalGrades.ContainsKey(sub.StudentName))
                            finalGrades[sub.StudentName] = new Dictionary<string, ModuleGrade>();

                        if (!finalGrades[sub.StudentName].ContainsKey(moduleName))
                            finalGrades[sub.StudentName][moduleName] = new ModuleGrade();

                        finalGrades[sub.StudentName][moduleName].Submissions.Add(sub);
                    }
                }

                // After processing all files for the module, calculate compiledSubmission and final scores
                foreach (var student in finalGrades.Values)
                {
                    foreach(var module in student.Values)
                    {
                        _gradeService.CompileLabSubmissions(module);
                    }
                }

            }

            string downloadsFolder = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Downloads");
            string fileName = "FinalGrades.csv";
            string fullPath = Path.Combine(downloadsFolder, fileName);
            _exportService.Export(fullPath, finalGrades);
        }
    }
}
