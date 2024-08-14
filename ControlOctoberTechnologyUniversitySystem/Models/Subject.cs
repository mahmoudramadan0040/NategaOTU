namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class Subject: BaseEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Subject_Code {  get; set; }
        public int CreditHours { get; set; }



        public bool IsGeneralSubject { get; set; }
        public int MaxScore { get; set; }
        public int MaxSemesterScore { get; set; }
        public int MaxFinalExamScore
        {
            get {

                return MaxScore - MaxSemesterScore;
            }
        }
        
        
       
        public virtual ICollection<StudentSubject>? StudentSubjects { get; set; }
        public virtual Department? Department { get; set; }
        // Nullable foreign key
        public Guid? DepartmentId { get; set; }
    }


}
