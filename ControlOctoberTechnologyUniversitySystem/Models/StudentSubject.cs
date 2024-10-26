using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class StudentSubject
    {
        [Key]
        public Guid Id { get; set; }
        public float SemesterScore { get; set; }
        public float FinalExamScore { get; set; }
        public float totalScore { get; set; }
        public string? grade { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        [JsonIgnore]
        public virtual Student? student { get; set; }
        [JsonIgnore]
        public virtual Subject? subject { get; set; }
    }

    
}
