namespace ControlOctoberTechnologyUniversitySystem.Models.Interfaces
{
    public interface IStudentRepo
    {
        public Task<IEnumerable<Student>> ImportStudentData(IFormFile file);
    }
}
