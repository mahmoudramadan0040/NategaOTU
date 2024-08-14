namespace ControlOctoberTechnologyUniversitySystem.Models.DTO
{
    public record StudentSubjectDto
    (
    float SemesterScore ,
    float FinalExamScore ,
    float totalScore ,
    string? grade ,
    Guid StudentId ,
    Guid SubjectId 
    );
}
