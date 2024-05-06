namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class StudentSubject
    {
        public Guid Id { get; set; }
        public float SemesterScore { get; set; }
        public float FinalExamScore { get; set; }
        public float totalScore { get; set; }
        public string grade { get; set; }
        public Student student { get; set; }
        public Department department { get; set; }
        public Subject subject { get; set; }


    }

    
}
