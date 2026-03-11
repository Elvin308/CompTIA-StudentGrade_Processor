using GradeProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProcessor.Infrastructure
{
    public interface IExport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">Location to export to</param>
        /// <param name="grades">Dictionary<StudentName, Dictionary<ModuleTitle, ModuleGrade>></param>
        public void Export(string filePath, Dictionary<string, Dictionary<string, ModuleGrade>> grades);
    }
}
