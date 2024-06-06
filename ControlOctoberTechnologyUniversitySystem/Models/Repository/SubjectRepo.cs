using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class SubjectRepo : ISubjectRepo
    {
        public readonly ControlDbContext _context;
        public SubjectRepo(ControlDbContext context)
        {
            _context = context;
        }

        public Subject CreateSubject(Subject subject)
        {
            throw new NotImplementedException();
        }

        public void DeleteSubject(Guid Id)
        {
            var subject = _context.Subjects.Find(Id);
            _context.Subjects.Remove(subject);
            _context.SaveChanges();
        }

        public Subject GetSubjectById(Guid Id)
        {
            return _context.Subjects.FirstOrDefault(x => x.Id == Id); 
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return _context.Subjects.ToList();
        }

        public IEnumerable<Student> GetSubjectStudents(Guid subjectId)
        {
            var students = _context.Students
                .Where(s => s.StudentSubjects
                .Any(e => e.SubjectId == subjectId))
                .ToList();
            return students;
        }   

        public async  Task StudentsEnrollSubject(Guid [] StudentIds, Guid[] SubjectIds)
        {
            // check all student is exists or not 
            foreach (var studentId in StudentIds)
            {
                var student = _context.Students.Find(studentId);
                if(student ==null)
                {
                    throw new ArgumentException($"student with this id => {studentId} not exists ");
                }

            }

            // check all subjects is exists or not 
            foreach (var subjectId in SubjectIds)
            {
                var subject = _context.Subjects.Find(subjectId);
                if (subject == null)
                {
                    throw new ArgumentException($"student with this id => {subjectId} not exists ");
                }
            }

            // check if the student is already enrolled in the subject 
            foreach(var studentId in StudentIds)
            {
                foreach(var subjectId in SubjectIds)
                {
                    var existingEnrollment = await _context.StudentSubjects
                        .FirstOrDefaultAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId);
                    if(existingEnrollment == null)
                    {
                        var studentSubject = new StudentSubject
                        {
                            StudentId = studentId,
                            SubjectId = subjectId,
                        };
                        _context.StudentSubjects.Add(studentSubject);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task StudentsUnrollSubject(Guid[] StudentIds, Guid[] SubjectIds)
        {
            // check all student is exists or not 
            foreach (var studentId in StudentIds)
            {
                var student = _context.Students.Find(studentId);
                if (student == null)
                {
                    throw new ArgumentException($"student with this id => {studentId} not exists ");
                }

            }

            // check all subjects is exists or not 
            foreach (var subjectId in SubjectIds)
            {
                var subject = _context.Subjects.Find(subjectId);
                if (subject == null)
                {
                    throw new ArgumentException($"student with this id => {subjectId} not exists ");
                }
            }
            // check if the student is already enrolled in the subject 
            foreach (var studentId in StudentIds)
            {
                foreach (var subjectId in SubjectIds)
                {
                    var StudentSubject = await _context.StudentSubjects
                        .FirstOrDefaultAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId);
                    if (StudentSubject != null)
                    {
                        _context.StudentSubjects.Remove(StudentSubject);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        public Subject UpdateSubject(Subject subject)
        {
            _context.Subjects.Update(subject);
            _context.SaveChanges();
            return subject;
        }
    }
}
