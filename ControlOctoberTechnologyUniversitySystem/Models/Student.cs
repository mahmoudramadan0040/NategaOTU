
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class Student: BaseEntity
    {
        public Guid Id { get; set; }
        public string? student_id { get; set; }  
        public string? student_setId { get; set; }
        public string? fullname { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? phone { get; set; }

        [AllowedValues("First","Second","Third","Forth")]
        public string? StudentStatus { get; set; }
        public bool? graduated { get; set; }
        [AllowedValues("Fresh", "RemainingOne", "RemainingTwo", "FirstChance", "SecondChance")]
        public string? StudentContraint { get; set; }
        public Guid? DepartmentId { get; set; }
        
        public virtual ICollection<Subject>? Subjects { get; set; }
        
        public virtual ICollection<StudentImage>? StudentImages { get; set; }
    }
    
    public class StudentImage
    {
        public Guid StudentImageId { get; set; } // Primary key

        [ForeignKey("StudentId")]
        public Guid? StudentId { get; set; } // Foreign key

        public string? ImageUrl { get; set; } // Image URL
    }
     

    
}
