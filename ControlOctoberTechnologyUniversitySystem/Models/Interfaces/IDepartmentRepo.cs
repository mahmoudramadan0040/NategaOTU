using ControlOctoberTechnologyUniversitySystem.Models.DTO;

namespace ControlOctoberTechnologyUniversitySystem.Models.Interfaces
{
    public interface IDepartmentRepo
    {
        public Department GetDepartment(Guid Id);
        public IEnumerable<Department> GetDepartments();
        public Department CreateDepartment(Department department);
        public void DeleteDepartment(Department department);
        public Department UpdateDepartment(Department department,Guid departmentId);
        public IEnumerable<Student> GetStudentsByDepatment(Guid departmentId);
        public IEnumerable<Subject> GetSubjectsByDepartment(Guid departmentId);
        public Task enrollStudentsInDepartment(Guid departmentId , Guid[] studentIds);
        public Task UnrollStudentsFromDepartment(Guid departmentId, Guid[] studentIds );
        public Task<List<DepartmentReportDto>> GetAllDepartmentsWithStatistics();
        public void addSubjectToDepartment(Guid departmentId, Guid subjectId);
        public void removeSubjectFromDepartment(Guid departmentId, Guid subjectId);


        /*public Task<Department> UpdateDepartment(Guid departmentId , );*/

    }
}
