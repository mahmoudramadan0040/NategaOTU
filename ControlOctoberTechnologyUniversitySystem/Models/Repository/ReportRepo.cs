using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using ControlOctoberTechnologyUniversitySystem.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class ReportRepo :IReportRepo
    {
        public readonly ControlDbContext _context;

        public ReportRepo(ControlDbContext context )
        {
            _context = context;
        }

        public async Task<List<object>> NategaReport(Guid departmentId, string studentStatus, [FromBody] Guid[] subjectIds)
        {
            try
            {
                if (subjectIds.Length == 0 && studentStatus is not null)
                {
                    var result = await _context.StudentSubjects
                        .Include(ss => ss.student) // Eager load Student
                        .Include(ss => ss.subject) // Eager load Subject
                        .Where(ss => ss.student.StudentStatus == studentStatus)
                        .GroupBy(ss => ss.SubjectId)
                        .Select(g => new
                        {
                            SubjectId = g.Key,
                            SubjectName = g.First().subject.Name,
                            students = g.Select(ss => new
                            {
                                StudentId = ss.StudentId,
                                StudentName = ss.student.fullname,
                                Grade = ss.grade,
                                SemesterScore = ss.SemesterScore,
                                FinalExamScore = ss.FinalExamScore,
                                TotalScore = ss.TotalScore
                            }).ToList()
                        }).ToListAsync();
                    return result.Cast<object>().ToList(); ;
                }
                else if (subjectIds.Length != 0 && studentStatus is not null)
                {
                    throw new Exception("studentStatus is null && subjectIds is null");

                }
                else
                {
                    throw new Exception("studentStatus is null && subjectIds is null");

                }
            }
            catch (Exception e)
            {
                throw new Exception("studentStatus is null && subjectIds is null");
            }
            

            

        }
    }
}
