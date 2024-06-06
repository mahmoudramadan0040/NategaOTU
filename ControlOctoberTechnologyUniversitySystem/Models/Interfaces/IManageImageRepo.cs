namespace ControlOctoberTechnologyUniversitySystem.Models.Interfaces
{
    public interface IManageImageRepo
    {
        Task<string> AddImage(IFormFile file);
        Task DeleteImage(StudentImage image);
        
            
        
    }
}
