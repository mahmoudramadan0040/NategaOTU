using ControlOctoberTechnologyUniversitySystem.BusinessLogic;
using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using static ControlOctoberTechnologyUniversitySystem.Controllers.GradeController;

namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class GradeRepo : IGradeRepo
    {
        private readonly ControlDbContext _context;
        private readonly IControllRole _controlRole;
        private readonly ILogger<GradeRepo> _logger;
        public GradeRepo(ControlDbContext context, IControllRole controlRole, ILogger<GradeRepo> logger)
        {
            _context = context;
            _controlRole = controlRole;

            _logger = logger;
        }


        public async Task<IEnumerable<StudentSubject>> FilterGradesInSubject(GradeFilter? filter,Guid subjectId)
        {
            IQueryable<StudentSubject> query = _context.StudentSubjects.Where(s=> s.subject.Id==subjectId);
            if (filter.graduated.HasValue)
                query =query.Where(s => s.student.graduated ==  filter.graduated);
            if (!string.IsNullOrEmpty(filter.StudentConstraint))
                query = query.Where(s => s.student.StudentContraint == filter.StudentConstraint );
            if (!string.IsNullOrEmpty(filter.StudentStatus)) 
                query =query.Where(s => s.student.StudentStatus == filter.StudentStatus);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<StudentSubject>> GetGradesInSubject(Guid subjectId)
        {
            IQueryable<StudentSubject> query = _context.StudentSubjects.Where(s=> s.subject.Id==subjectId);
            return await query.ToListAsync();
        }
        public async Task<List<StudentSubject>> AddStudentGrade(List<StudentSubject> studentsSubjects, Guid subjectId)
        {
            var resultList = new List<StudentSubject>();  // To store the result (existing or new grades)
            // get the subject information to use right calculate
            var subjectInfo = _context.Subjects.FirstOrDefault(s => s.Id == subjectId) 
                ?? throw new ArgumentException($"Subject with this id => {subjectId} not exists ");

            if (studentsSubjects == null || studentsSubjects.Count == 0)
                _logger.LogWarning("No student subjects provided.");
            
            
            foreach (StudentSubject studentSubject in studentsSubjects)
            {
                var existingGrade = _context.StudentSubjects
                    .Where(s => s.SubjectId == subjectId && s.StudentId == studentSubject.StudentId)
                    .FirstOrDefault();

                if ( existingGrade is not null)
                {

                    existingGrade.FinalExamScore= studentSubject.FinalExamScore;
                    existingGrade.SemesterScore= studentSubject.SemesterScore; 
                    existingGrade.TotalScore= studentSubject.TotalScore;
                    if (subjectInfo.IsGeneralSubject)
                        if(studentSubject.FinalExamScore.ValueKind == JsonValueKind.Number)
                            existingGrade.grade = _controlRole.CalclateGeneralGrade(subjectInfo.MaxScore, studentSubject.FinalExamScore.GetSingle(), studentSubject.SemesterScore.GetSingle());
                        else
                            existingGrade.grade = _controlRole.StatusOther(studentSubject.FinalExamScore.GetString() ?? throw new ArgumentNullException(" final exam can not be null ! ") );
                    else
                        if (studentSubject.FinalExamScore.ValueKind == JsonValueKind.Number)
                            existingGrade.grade = _controlRole.CalculateGrade(subjectInfo.MaxScore, studentSubject.FinalExamScore.GetSingle(), studentSubject.SemesterScore.GetSingle());
                        else
                            existingGrade.grade = _controlRole.StatusOther(studentSubject.FinalExamScore.GetString() ?? throw new ArgumentNullException(" final exam can not be null ! "));

                    
                    _context.StudentSubjects.Update(existingGrade);
                    resultList.Add(existingGrade);


                }
                else
                {
                    if (subjectInfo.IsGeneralSubject)
                        if (studentSubject.FinalExamScore.ValueKind == JsonValueKind.Number)
                            studentSubject.grade = _controlRole.CalclateGeneralGrade(subjectInfo.MaxScore, studentSubject.FinalExamScore.GetSingle(), studentSubject.SemesterScore.GetSingle());
                        else
                            studentSubject.grade = _controlRole.StatusOther(studentSubject.FinalExamScore.GetString() ?? throw new ArgumentNullException(" final exam can not be null ! "));
                    else
                        if (studentSubject.FinalExamScore.ValueKind == JsonValueKind.Number)
                            studentSubject.grade = _controlRole.CalculateGrade(subjectInfo.MaxScore, studentSubject.FinalExamScore.GetSingle(), studentSubject.SemesterScore.GetSingle());
                        else
                            studentSubject.grade = _controlRole.StatusOther(studentSubject.FinalExamScore.GetString() ?? throw new ArgumentNullException(" final exam can not be null ! "));
                    
                    _context.StudentSubjects.Add(studentSubject);
                    resultList.Add(studentSubject);
                }
                                   
            }
            await _context.SaveChangesAsync();
            return resultList;
        }





















        public void DeleteStudentGrade(Guid studentId, Guid subjectId)
        {
            var studentsubject= _context.StudentSubjects.FirstOrDefault(s => s.SubjectId == subjectId && s.StudentId == studentId);
            if(studentsubject != null)
            {
                _context.StudentSubjects.Remove(studentsubject);
                _context.SaveChanges();
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
