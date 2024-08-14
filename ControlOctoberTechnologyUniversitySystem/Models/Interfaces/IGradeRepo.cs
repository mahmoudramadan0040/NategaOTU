namespace ControlOctoberTechnologyUniversitySystem.Models.Interfaces
{
    public interface IGradeRepo
    {

        // student get all grades in subjects 
        // student add grade to subject
        // student add 
        public IEnumerable<StudentSubject> GetStudentGradesInSubjects(Guid studentId);
        public StudentSubject GetStudentGradeInSubject(Guid studentId, Guid subjectId);
        public StudentSubject AddStudentGrade(StudentSubject studentSubject);
        public void DeleteStudentGrade(Guid studentId,Guid subjectId);
        public StudentSubject UpdateStudentGrade(StudentSubject studentSubject);


    }
}
