using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class ReportRepo :IReportRepo
    {
        public readonly ControlDbContext _context;

        public ReportRepo(ControlDbContext context )
        {
            _context = context;
        }

        public async Task<List<object>> SubjectsReports(Guid departmentId, string studentStatus, Guid[]? subjectIds)
        {
            try
            {

                // Validate studentStatus and subjectIds
                if (string.IsNullOrEmpty(studentStatus))
                {
                    throw new ArgumentException("studentStatus cannot be null or empty.");
                }

                if (subjectIds == null || subjectIds.Length == 0)
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
                            Id = ss.StudentId,
                            StudentName = ss.student.fullname,
                            StudentContraint = ss.student.StudentContraint,
                            Student_setId = ss.student.student_setId,
                            StudentId=ss.student.student_id,
                            Grade = ss.grade,
                            // Ensure SemesterScore is not null or invalid
                            SemesterScore = ss.SemesterScoreJson,
                            // Ensure FinalExamScore is not null or invalid
                            FinalExamScore = ss.FinalExamScoreJson,
                            // Ensure TotalScore is not null or invalid
                            TotalScore = ss.TotalScoreJson
                        }).ToList()
                    }).ToListAsync();
                    return result.Cast<object>().ToList(); ;
                }else{
                    var result = await _context.StudentSubjects
                    .Include(ss => ss.student) // Eager load Student
                    .Include(ss => ss.subject) // Eager load Subject
                    .Where(ss => ss.student.StudentStatus == studentStatus)
                    .Where(ss=>subjectIds.Contains(ss.SubjectId))
                    .GroupBy(ss => ss.SubjectId)
                    .Select(g => new
                    {
                        SubjectId = g.Key,
                        SubjectName = g.First().subject.Name,
                        students = g.Select(ss => new
                        {
                            Id = ss.StudentId,
                            StudentName = ss.student.fullname,
                            StudentContraint = ss.student.StudentContraint,
                            Student_setId = ss.student.student_setId,
                            StudentId=ss.student.student_id,
                            Grade = ss.grade,
                            // Ensure SemesterScore is not null or invalid
                            SemesterScore = ss.SemesterScoreJson,
                            // Ensure FinalExamScore is not null or invalid
                            FinalExamScore = ss.FinalExamScoreJson,
                            // Ensure TotalScore is not null or invalid
                            TotalScore = ss.TotalScoreJson
                        }).ToList()
                    }).ToListAsync();
                    return result.Cast<object>().ToList(); ;
                }
                
                
            }
            catch (Exception e)
            {
                throw new Exception("studentStatus is null && subjectIds is null");
            }
        }

        public async Task<List<object>> NategaReport(Guid departmentId, string studentStatus, Guid[]? subjectIds)
        {
            try
            {

                // Validate studentStatus and subjectIds
                if (string.IsNullOrEmpty(studentStatus))
                {
                    throw new ArgumentException("studentStatus cannot be null or empty.");
                }
                if (subjectIds == null || subjectIds.Length == 0)
                {
                    var result = await _context.StudentSubjects
                    .Include(ss => ss.student) // Eager load Student
                    .Include(ss => ss.subject) // Eager load Subject
                    .Where(ss => ss.student.StudentStatus == studentStatus)
                    .GroupBy(ss => ss.StudentId)
                    .Select(g => new
                    {
                        Id = g.Key,
                        StudentName = g.First().student.fullname,
                        StudentConstraint = g.First().student.StudentContraint,
                        StudentSetId = g.First().student.student_setId,
                        StudentId = g.First().student.student_id,
                        Subjects = g.Select(ss => new
                        {
                            SubjectId = ss.SubjectId,
                            SubjectName = ss.subject.Name,
                            Grade = ss.grade,
                            // Ensure SemesterScore is not null or invalid
                            SemesterScore = ss.SemesterScoreJson,
                            // Ensure FinalExamScore is not null or invalid
                            FinalExamScore = ss.FinalExamScoreJson,
                            // Ensure TotalScore is not null or invalid
                            TotalScore = ss.TotalScoreJson
                        }).ToList()
                    }).ToListAsync();
                return result.Cast<object>().ToList(); ;
                }
                else{
                    var result = await _context.StudentSubjects
                    .Include(ss => ss.student) // Eager load Student
                    .Include(ss => ss.subject) // Eager load Subject
                    .Where(ss => ss.student.StudentStatus == studentStatus)
                    .Where(ss=>subjectIds.Contains(ss.SubjectId))
                    .GroupBy(ss => ss.StudentId)
                    .Select(g => new
                    {
                        Id = g.Key,
                        StudentName = g.First().student.fullname,
                        StudentConstraint = g.First().student.StudentContraint,
                        StudentSetId = g.First().student.student_setId,
                        StudentId = g.First().student.student_id,
                        Subjects = g.Select(ss => new
                        {
                            SubjectId = ss.SubjectId,
                            SubjectName = ss.subject.Name,
                            Grade = ss.grade,
                            // Ensure SemesterScore is not null or invalid
                            SemesterScore = ss.SemesterScoreJson,
                            // Ensure FinalExamScore is not null or invalid
                            FinalExamScore = ss.FinalExamScoreJson,
                            // Ensure TotalScore is not null or invalid
                            TotalScore = ss.TotalScoreJson
                        }).ToList()
                    }).ToListAsync();
                    return result.Cast<object>().ToList(); ;
                }
                
                
            }
            catch (Exception e)
            {
                throw new Exception("studentStatus is null && subjectIds is null");
            }
        }
    }
}
