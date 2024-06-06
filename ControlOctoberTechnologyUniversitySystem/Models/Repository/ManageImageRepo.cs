using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;

namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class ManageImageRepo:IManageImageRepo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ManageImageRepo> _logger;
        public ManageImageRepo(IHttpContextAccessor httpContextAccessor, ILogger<ManageImageRepo> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<string> AddImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("Invalid file or empty file content.");
                // Create the directory if it doesn't exist
                _logger.LogInformation("before the combine path ");

                var uploadPath = Path.Combine("Resources", "Images");
                _logger.LogDebug(uploadPath.ToString());
                Directory.CreateDirectory(uploadPath);
                // Generate a unique file name (you can customize this logic)
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);
                // Save the file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                // Get the base URL of the application
                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

                // Construct the accessible URL
                var imageUrl = $"{baseUrl}/Resources/Images/{fileName}";

                return imageUrl;
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., logging, error messages, etc.)
                throw new ApplicationException($"Error storing file: {ex}");
            }
        }

        public async Task DeleteImage(StudentImage image)
        {
            try
            {

                if (System.IO.File.Exists(image.ImageUrl))
                {
                    System.IO.File.Delete(image.ImageUrl);
                }
                else
                {
                    // Handle case where the image file doesn't exist
                    throw new FileNotFoundException($"Image file '{image.ImageUrl}' not found.");
                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., logging, error messages, etc.)
                throw new ApplicationException($"Error deleting file: {ex}");
            }
        }
    }
}
