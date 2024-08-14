using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using ControlOctoberTechnologyUniversitySystem.Utils.Interfaces;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using static ControlOctoberTechnologyUniversitySystem.Controllers.StudentController;

namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class StudentRepo : IStudentRepo
    {
        public readonly ControlDbContext _context;
        public readonly IManageExcelFiles _manageExcelFiles;
        public  StudentRepo(ControlDbContext context ,IManageExcelFiles manageExcelFiles )
        {
            _context = context;
            _manageExcelFiles = manageExcelFiles;
        }
        public IEnumerable<Student> GetStudents()
        {
            return _context.Students.AsNoTracking();
        }
        public Student CreateStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;

        }

        public void DeleteStudent(Guid Id)
        {
            var result = _context.Students.Find(Id);
            _context.Students.Remove(result);
            _context.SaveChanges();
        }

        public Student GetStudentById(Guid Id)
        {
            return _context.Students.Where(s => s.Id == Id).FirstOrDefault();
        }

        public async Task<IEnumerable<Student>> ImportStudentData(IFormFile file)
        {
            var data =  _manageExcelFiles.ImportStudentDataFromExcel(file);
            _context.Students.AddRange(data);
            await _context.SaveChangesAsync();
            return data;

        }

        public Student UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
            return student;
        }
        public async Task<IEnumerable<Student>> GetFilterStudentsAsync(StudentFilter? filter)
        {
            IQueryable<Student> query = _context.Students;
            if (filter.graduated.HasValue)
                query =query.Where(s => s.graduated ==  filter.graduated);
            if (!string.IsNullOrEmpty(filter.StudentConstraint))
                query = query.Where(s => s.StudentContraint == filter.StudentConstraint );
            if (!string.IsNullOrEmpty(filter.StudentStatus)) 
                query =query.Where(s => s.StudentStatus == filter.StudentStatus);
            return await query.ToListAsync();
        }



    }
}
