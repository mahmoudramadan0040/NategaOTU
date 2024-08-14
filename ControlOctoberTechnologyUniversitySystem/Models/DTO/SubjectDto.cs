namespace ControlOctoberTechnologyUniversitySystem.Models.DTO
{
    public record SubjectDto
    (
        string? Name,
        string? Subject_Code,
        int CreditHours,
        bool IsGeneralSubject,
        int MaxScore,
        int MaxSemesterScore


    );
}
