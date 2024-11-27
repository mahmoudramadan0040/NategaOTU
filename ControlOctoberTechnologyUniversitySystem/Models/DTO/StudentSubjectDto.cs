using System.Text.Json;

namespace ControlOctoberTechnologyUniversitySystem.Models.DTO
{
    public record StudentSubjectDto
    (
    JsonElement? SemesterScore ,
    JsonElement? FinalExamScore ,
    JsonElement? totalScore ,
    // string? grade ,
    Guid? StudentId ,
    Guid? SubjectId 
    );
}
