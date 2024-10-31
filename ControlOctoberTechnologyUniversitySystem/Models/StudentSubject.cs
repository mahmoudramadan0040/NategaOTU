using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class StudentSubject
    {
        [Key]
        public Guid Id { get; set; }
        // Backing string properties for JSON data
        [JsonIgnore]
        public string SemesterScoreJson { get; set; } = string.Empty; // Store raw JSON string
        [JsonIgnore]
        public string FinalExamScoreJson { get; set; } = string.Empty; // Store raw JSON string
        [JsonIgnore]
        public string TotalScoreJson { get; set; } = string.Empty; // Store raw JSON string


        // JsonElement properties to work with in application code
        [NotMapped]
        public JsonElement SemesterScore
        {
            get => JsonSerializer.Deserialize<JsonElement>(SemesterScoreJson);
            set => SemesterScoreJson = JsonSerializer.Serialize(value);
        }
        [NotMapped]
        public JsonElement FinalExamScore
        {
            get => JsonSerializer.Deserialize<JsonElement>(FinalExamScoreJson);
            set => FinalExamScoreJson = JsonSerializer.Serialize(value);
        }
        
        [NotMapped]
        public JsonElement TotalScore
        {
            get => JsonSerializer.Deserialize<JsonElement>(TotalScoreJson);
            set => TotalScoreJson = JsonSerializer.Serialize(value);
        }

        public string? grade { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        [JsonIgnore]
        public virtual Student? student { get; set; }
        [JsonIgnore]
        public virtual Subject? subject { get; set; }
    }

    
}
