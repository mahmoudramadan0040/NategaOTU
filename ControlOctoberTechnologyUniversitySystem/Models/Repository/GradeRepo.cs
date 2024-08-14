using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;

namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class GradeRepo : IGradeRepo
    {
        private readonly ControlDbContext _context;
        public GradeRepo(ControlDbContext context) {
            _context = context;
        }
        public StudentSubject AddStudentGrade(StudentSubject studentSubject)
        {
            _context.StudentSubjects.Add(studentSubject);
            _context.SaveChanges();
            return studentSubject;

        }

        public void DeleteStudentGrade(Guid studentId, Guid subjectId)
        {
            var studentsubject= _context.StudentSubjects.FirstOrDefault(s => s.SubjectId == subjectId && s.StudentId == studentId);
            if(studentsubject != null)
            {
                _context.StudentSubjects.Remove(studentsubject);
            }
            else
            {
                throw new Exception("Can not delete student grade !"); 
            }
        }

        public StudentSubject GetStudentGradeInSubject(Guid studentId, Guid subjectId)
        {
             var studentGrade =  _context.StudentSubjects
                .Where(s => s.SubjectId == subjectId && s.StudentId == studentId).FirstOrDefault();
            return studentGrade;
        }

        public IEnumerable<StudentSubject> GetStudentGradesInSubjects(Guid studentId)
        {
            return _context.StudentSubjects
                .Where(s => s.StudentId == studentId).ToList();
        }

        public StudentSubject UpdateStudentGrade(StudentSubject studentSubject)
        {
            _context.StudentSubjects.Update(studentSubject);
            _context.SaveChanges();
            return studentSubject;
        }
    }
}
