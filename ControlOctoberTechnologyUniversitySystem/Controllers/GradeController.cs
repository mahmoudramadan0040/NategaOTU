using AutoMapper;
using ControlOctoberTechnologyUniversitySystem.Models;
using ControlOctoberTechnologyUniversitySystem.Models.DTO;
using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using ControlOctoberTechnologyUniversitySystem.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace ControlOctoberTechnologyUniversitySystem.Controllers
{
    [Route("api/grades")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly ILogger<GradeController> _logger;
        private readonly IMapper _mapper;
        private readonly IGradeRepo _gradeRepo;
        public GradeController(ILogger<GradeController> logger, IMapper mapper, IGradeRepo gradeRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _gradeRepo = gradeRepo;
        }

        [HttpGet("student/{studentId}")]
        public IActionResult GetStudentGrades(Guid studentId)
        {
            try
            {
                var grades = _gradeRepo.GetStudentGradesInSubjects(studentId);

                if (grades == null)
                    return NotFound();
                return Ok(grades);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get category  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("grade/student/{studentId}/subject/{subjectId}")]
        public IActionResult GetStudentGradeInSubject(Guid studentId, Guid subjectId)
        {
            try
            {
                var grades = _gradeRepo.GetStudentGradeInSubject(studentId,subjectId);

                if (grades == null)
                    return NotFound();
                return Ok(grades);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get category  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{subjectId}")]
        public async Task<IActionResult> AddStudentGrade(StudentSubjectDto[] StudentSubjects , Guid subjectId)
        {
            try
            {
                List<StudentSubject> studentSubjects = new List<StudentSubject>();
                if (StudentSubjects == null)
                    return BadRequest();
                foreach(var studentSubject in StudentSubjects)
                {
                    var StudentSubjectMap = _mapper.Map<StudentSubject>(studentSubject);

                    studentSubjects.Add(StudentSubjectMap);
                }
                var result =await _gradeRepo.AddStudentGrade(studentSubjects,subjectId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not add student Grade : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("student/{studentId}/subject/{subjectId}")]
        public IActionResult DeleteGrade(Guid studentId , Guid subjectId)
        {
            try
            {
                
                _gradeRepo.DeleteStudentGrade(studentId, subjectId);
                return Accepted("Grade is deleted successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get category  : {ex}");
                return StatusCode(404, $"Can not delete Subject With Id: {subjectId} and student with Id : {studentId} !");
            }
        }
        


    }

}
