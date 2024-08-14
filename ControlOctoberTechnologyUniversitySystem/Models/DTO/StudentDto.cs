namespace ControlOctoberTechnologyUniversitySystem.Models.DTO
{
    public record StudentDto
    (
         string? student_id ,
         string? student_setId ,
         string? fullname ,
         string? firstname ,
         string? lastname ,
         string? phone ,
         string? StudentStatus ,
         bool? graduated ,
         string? StudentContraint ,
         IFormFile[]? StudentImage
    );
}
