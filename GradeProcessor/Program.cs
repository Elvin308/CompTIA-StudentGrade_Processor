using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GradeProcessor.Services;
using GradeProcessor.Infrastructure;
using GradeProcessor.Application;


var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string rootFolder = config["GradeRootFolder"];
var penaltyFiles = config.GetSection("PenaltyFiles").Get<Dictionary<string, int>>();
var penalties = config.GetSection("Penalties").Get<Dictionary<int, double>>();

var services = new ServiceCollection();

services.AddSingleton(penaltyFiles);
services.AddSingleton(penalties);

services.AddTransient<IImport, CsvImportService>();
services.AddTransient<IExport, CsvExportService>();
services.AddTransient<PenaltyService>();
services.AddTransient<GradeCalculatorService>();


services.AddTransient<GradeProcessorApp>();
var provider = services.BuildServiceProvider();

var processor = provider.GetRequiredService<GradeProcessorApp>();
processor.MainProgram(rootFolder);


Console.WriteLine("Grade processing completed.");