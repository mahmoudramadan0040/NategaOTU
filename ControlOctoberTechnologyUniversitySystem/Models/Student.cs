namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class Student: BaseEntity
    {
        public Guid Id { get; set; }
        public string student_id { get; set; }  
        public string student_setId { get; set; }
        public string fullname { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public StudentStatus StudentStatus { get; set; }
        public bool graduated { get; set; }
        public StudentContraint StudentContraint { get; set; }
        public string image { get; set; }

        public Department Department { get; set; } 
        public Specialization  Specialization { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
        
    }
    

    public enum StudentStatus
    {
        First,
        Second,
        Third,
        Forth
    } 

    public enum StudentContraint
    {
        Fresh,
        RemainingOne,
        RemainingTwo,
        FirstChance, 
        SecondChance
    }
}
