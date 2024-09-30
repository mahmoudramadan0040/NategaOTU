using ControlOctoberTechnologyUniversitySystem.Models.DTO;
using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using MathNet.Numerics.Distributions;
using Microsoft.EntityFrameworkCore;

namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly ControlDbContext _context;
        public DepartmentRepo(ControlDbContext context)
        {
            _context = context;
        }
        

        public Department CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public void DeleteDepartment(Department department)
        {
            _context.Departments.Remove(department);
            _context.SaveChanges();
        }

        public async Task enrollStudentsInDepartment(Guid departmentId, Guid[] studentIds)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == departmentId);
            if (department == null)
                throw new ArgumentException($"department with this id => {departmentId} not exists ");
            // check all students is exists or not 
            foreach (var studentId in studentIds)
            {
                var student =  _context.Students.Find(studentId);
                if (student == null)
                {
                    throw new ArgumentException($"student with this id => {studentId} not exists ");
                }
                student.DepartmentId = departmentId;
            }
            await _context.SaveChangesAsync();


        }
        public async Task UnrollStudentsFromDepartment(Guid departmentId, Guid[] studentIds)
        {
            /*Console.WriteLine(studentIds[1]);*/
            var department = _context.Departments.FirstOrDefault(d => d.Id == departmentId);
            if (department == null)
                throw new ArgumentException($"department with this id => {departmentId} not exists ");
            foreach (var studentId in studentIds)
            {
                Console.WriteLine("------------------------------"+studentId.ToString());
                var student = _context.Students.Find(studentId);
                if (student == null)
                {
                    throw new ArgumentException($"student with this id => {studentId} not exists ");
                }

                student.DepartmentId = null;
            }
            await _context.SaveChangesAsync();


        }
        public void addSubjectToDepartment(Guid departmentId, Guid subjectId)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == departmentId);
            if (department == null)
                throw new ArgumentException($"department with this id => {departmentId} not exists ");

            var subject = _context.Subjects.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null)
                throw new ArgumentException($"subject with this id => {subjectId} not exists ");
            var existingEnroll = _context.Subjects.FirstOrDefault(s => s.Department.Id == departmentId);
            if (existingEnroll == null)
            {
                subject.Department = department;
                _context.SaveChanges();
            }
        }
        public void removeSubjectFromDepartment(Guid departmentId, Guid subjectId)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == departmentId);
            if (department == null)
                throw new ArgumentException($"department with this id => {departmentId} not exists ");

            var subject = _context.Subjects.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null)
                throw new ArgumentException($"subject with this id => {subjectId} not exists ");
            var existingEnroll = _context.Subjects.FirstOrDefault(s => s.Department.Id == departmentId);
            if (existingEnroll != null)
            {
                subject.Department = null;
                _context.SaveChanges();
            }

        }
        public Department GetDepartment(Guid Id)
        {
            return _context.Departments.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments;
        }

        public IEnumerable<Student> GetStudentsByDepatment(Guid departmentId)
        {
            return _context.Students
                .Where(x => x.DepartmentId == departmentId);
            
        }

        public IEnumerable<Subject> GetSubjectsByDepartment(Guid departmentId)
        {
            return _context.Subjects.Where(x => x.Department.Id==departmentId);

        }

        public Department UpdateDepartment(Department department,Guid departmentId)
        {
            department.Id = departmentId;

            var currentDepartment = _context.Departments.Find(departmentId) ?? throw new Exception($"can not find any subject with this id : {departmentId}");
            _context.Entry(currentDepartment).CurrentValues.SetValues(department);
            _context.Departments.Update(currentDepartment);
            _context.SaveChanges();
            return department;
        }

        public  Task<List<DepartmentReportDto>> GetAllDepartmentsWithStatistics()
        {
            var report = _context.Departments
                .Include(d => d.Students)
                .Include(d => d.Subjects)
                .Select(d => new DepartmentReportDto
                {
                departmentId = d.Id,
                departmentName = d.Name,
                numberOfStudents = d.Students.Count(),
                numberOfSubjects = d.Subjects.Count()
            }).ToListAsync();

            return report;
            
        }

        
    }
}
