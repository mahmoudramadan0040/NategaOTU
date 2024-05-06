namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Specialization> Specializations { get; set; }
    }
}
