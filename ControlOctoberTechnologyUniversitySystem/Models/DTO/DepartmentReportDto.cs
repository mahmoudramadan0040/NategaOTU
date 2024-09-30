namespace ControlOctoberTechnologyUniversitySystem.Models.DTO
{
    public class DepartmentReportDto
    {
        public Guid departmentId { get; set; }
        public string departmentName { get; set; }
        public int numberOfStudents { get; set; }
        public int numberOfSubjects { get; set; }
    }
}
