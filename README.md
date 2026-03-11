# GradeProcessor – CompTIA A+ Lab Grading Tool

## Overview
**GradeProcessor** is a C# console application designed to automate the grading process for CompTIA A+ course labs. It imports CSV lab reports, applies late submission penalties, calculates module averages, and exports final grades in a single CSV.

---

## Features
- Imports lab grades from CSV files.
- Determines the **best lab submission per student**, considering multiple resubmissions.
- Applies configurable **late submission penalties** (up to 3 weeks late).
- Handles modules with **varying lab assignments** automatically.
- Calculates final **module averages** per student.
- Exports final grades in CSV format.
- Configurable via `appsettings.json` (grade folders, penalties, and file mappings).

---

## Architecture
- **Application** -> Main application program
- **Services** -> Business logic (GradeCalculator, PenaltyService)
- **Domain** -> Entities (ModuleGrade, LabSubmission, Student)
- **Infrastructure** -> Import/Export logic

---

## Configuration

`appsettings.json` example:

```json
{
  "GradeRootFolder": "C:\\CourseGrades",
  "PenaltyFiles": {
    "Module2": 0,
    "Module3": 1,
    "Module4": 2
  },
  "Penalties": {
    "0": 0.0,
    "1": 0.1,
    "2": 0.2,
    "3": 0.3
  }
}
```

- GradeRootFolder – Folder containing module subfolders with CSV exports.
- PenaltyFiles – Optional mapping of module to specific late penalty rules.
- Penalties – Penalty value per week late.

---
## Usage

1. Clone the repository:

```bash
git clone https://github.com/yourusername/GradeProcessor.git
```

2. Open in Visual Studio 2026 or newer.
3. Configure appsettings.json with your grading folders and penalty rules.
4. Build and run the console application.
5. The application will process all module folders, apply penalties, calculate grades, and export a single CSV with final scores.

---
## Sample Workflow

### Expected Folder Structure

GradeProcessor expects a root folder (configured in `appsettings.json` as `GradeRootFolder`) containing subfolders for each module. Each module folder contains the exported CSV files from CompTIA.org.
```
GradeRootFolder/
│
├── Module2/
│ ├── before_due.csv
│ ├── wee1.csv
| ├── wee2.csv
│ └── week3.csv
│
├── Module3/
│ ├── before_due.csv
│ ├── wee1.csv
| ├── wee2.csv
│ └── week3.csv
│
├── Module4/
│ ├── before_due.csv
│ ├── wee1.csv
| ├── wee2.csv
│ └── week3.csv
│
└── Module5/
│ ├── before_due.csv
│ ├── wee1.csv
| ├── wee2.csv
│ └── week3.csv
```

**Notes:**
- Csv file may have **varying quantity of labs**; the program will automatically handle modules with any lab quantity.
- Each CSV file should follow the **export format from CompTIA.org**, containing student names, labs and lab scores.
- The program will process **all module folders under the root folder automatically**.


### Flow
1. CSV files for each module are exported from CompTIA.org.
2. `GradeProcessor` imports the CSVs and groups submissions by student.
3. For each lab, the **highest adjusted score** (after applying any late penalties) is selected.
4. Module averages are calculated for each student.
5. Final grades are exported in a single CSV file.


### Example Output CSV

| Student Name | Module 2 | Module 3 | Module 4 | ... |
|--------------|----------|----------|----------|-----|
| John Doe     | 95%      | 88%      | 92%      |     |
| Jane Smith   | 100%     | 90%      | 85%      |     |
| Larry Fisher | 92%      | 87%      | 93%      |     |

---
## Dependency
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Configuration.Json
- CSV parsing library (CsvHelper)
