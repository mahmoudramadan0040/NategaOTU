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
         StudentStatus? StudentStatus ,
         bool? graduated ,
         StudentContraint StudentContraint ,
         IFormFile [] StudentImage
    );
}
