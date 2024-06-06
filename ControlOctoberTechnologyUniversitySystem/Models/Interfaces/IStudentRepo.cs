using static ControlOctoberTechnologyUniversitySystem.Controllers.StudentController;

namespace ControlOctoberTechnologyUniversitySystem.Models.Interfaces
{
    public interface IStudentRepo
    {
        public Task<IEnumerable<Student>> ImportStudentData(IFormFile file);
        public Student GetStudentById(Guid Id);

        Student CreateStudent(Student student);
        Student UpdateStudent(Student student);
        public void DeleteStudent(Guid Id);
        public IEnumerable<Student> GetStudents();

        public Task<IEnumerable<Student>> GetFilterStudentsAsync(StudentFilter filter);
    }
}
