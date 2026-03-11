using GradeProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProcessor.Infrastructure
{
    public interface IImport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">Root location of files</param>
        /// <param name="moduleName">Module title</param>
        /// <param name="penaltyWeek">Weeks past due date</param>
        /// <returns></returns>
        public List<LabSubmission> Import(string filePath, string moduleName, int penaltyWeek);
    }
}
