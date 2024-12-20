﻿using static ControlOctoberTechnologyUniversitySystem.Controllers.GradeController;

namespace ControlOctoberTechnologyUniversitySystem.Models.Interfaces
{
    public interface IGradeRepo
    {

        // student get all grades in subjects 
        // student add grade to subject
        // student add 
        public Task<IEnumerable<StudentSubject>> FilterGradesInSubject(GradeFilter? filter,Guid subjectId);
        public Task<IEnumerable<StudentSubject>> GetGradesInSubject(Guid subjectId);
        public IEnumerable<StudentSubject> GetStudentGradesInSubjects(Guid studentId);
        public StudentSubject GetStudentGradeInSubject(Guid studentId, Guid subjectId);
        public Task<List<StudentSubject>> AddStudentGrade(List<StudentSubject> studentsSubjects, Guid subjectId);
        public void DeleteStudentGrade(Guid studentId,Guid subjectId);
        public StudentSubject UpdateStudentGrade(StudentSubject studentSubject);



    }
}
