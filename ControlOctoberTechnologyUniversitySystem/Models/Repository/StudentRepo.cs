using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;

namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class StudentRepo : IStudentRepo
    {
        public readonly ControlDbContext _context;
        public  StudentRepo(ControlDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>> ImportStudentData(IFormFile file)
        {
                 Student a = new Student { 
                    CreatedDate = DateTime.Now ,
                };
            return (IEnumerable<Student>)a;

        }
    }
}
