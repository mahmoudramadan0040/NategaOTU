namespace ControlOctoberTechnologyUniversitySystem.Models.DTO
{
    public record SubjectDto
    (
        string? Name,
        string? Subject_code,
        int CreditHours,
        bool IsGeneralSubject,
        int MaxScore,
        int MaxSemesterScore,
        Department Department


    );
}
