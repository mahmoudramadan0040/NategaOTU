namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<Subject>? Subjects { get; set; }
        
    }
}
