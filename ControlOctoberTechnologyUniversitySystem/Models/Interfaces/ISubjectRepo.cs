namespace ControlOctoberTechnologyUniversitySystem.Models.Interfaces
{
    public interface ISubjectRepo
    {
        public Subject GetSubjectById(Guid Id);
        public Subject CreateSubject(Subject subject);
        public Subject UpdateSubject(Subject subject,Guid subjectId);
        public void DeleteSubject(Guid Id);
        public IEnumerable<Subject> GetSubjects();
        public IEnumerable<Student> GetSubjectStudents(Guid subjectId);
        public  Task StudentsEnrollSubject(Guid[] StudentIds , Guid [] SubjectIds);

        public Task StudentsUnrollSubject(Guid[] StudentIds, Guid[] SubjectIds);

        

    }
}
