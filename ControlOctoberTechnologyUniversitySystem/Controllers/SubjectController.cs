using AutoMapper;
using ControlOctoberTechnologyUniversitySystem.Models;
using ControlOctoberTechnologyUniversitySystem.Models.DTO;
using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace ControlOctoberTechnologyUniversitySystem.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        
        private readonly ISubjectRepo _subjectRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<SubjectController> _logger;
        public SubjectController(
            ISubjectRepo subjectRepo,
            IMapper mapper,
            ILogger<SubjectController> logger)
        {
            _subjectRepo = subjectRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public class Enroll
        {
           public Guid[] StudentIds { get; set; } = new Guid[] { };
           public Guid[] SubjectIds { get; set; } = new Guid[] { };
        }
        [HttpGet]
        public ActionResult<IEnumerable<Subject>> Getsubject()
        {
            try
            {
                var subjects = _subjectRepo.GetSubjects();
                if (subjects == null)
                    return NotFound();
                return Ok(subjects);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            
        }
        [HttpGet("subject/{subjectId}")]
        public ActionResult<Subject> GetById(Guid subjectId)
        {
            try
            {
                var subject = _subjectRepo.GetSubjectById(subjectId);
                if (subject == null)
                    return NotFound();
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("subject/{SubjectId}/students")]
        public ActionResult<Student> GetAllStudentBySubject(Guid SubjectId)
        {
            try
            {
                var subject = _subjectRepo.GetSubjectById(SubjectId);
                if(subject == null)
                    return NotFound($"Subject with id : {SubjectId} is not exists !");
                var students = _subjectRepo.GetSubjectStudents(SubjectId);
                if(students == null)
                    return NotFound();
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not delete Subject : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("subject")]
        public IActionResult CreateSubject(SubjectDto subject)
        {
            try
            {
                if(subject ==null)
                {
                    return BadRequest("subject object is null ");
                }
                if (!ModelState.IsValid)
                {
                    
                    return BadRequest("Invalid subject object !");
                }
                var subjectsMap = _mapper.Map<Subject>(subject); 
                var result =_subjectRepo.CreateSubject(subjectsMap);
                return Ok(result);

            }
            catch(Exception ex)
            {
                _logger.LogError($"can not create Subject : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Enrolls students in subjects.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/subjects/enroll
        ///     {
        ///         "studentIds": ["guid1", "guid2"],
        ///         "subjectIds": ["guid3", "guid4"]
        ///     }
        ///
        /// </remarks>
        /// <param name="enroll">The enrollment details.</param>
        /// <returns>Returns the result of the enrollment process.</returns>
        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudentsSubjects([FromBody] Enroll enroll )
        {
            try
            {
                if (enroll.StudentIds == null || enroll.SubjectIds == null)
                    return BadRequest("students ids or subject id may be empty !");
                await _subjectRepo.StudentsEnrollSubject(enroll.StudentIds, enroll.SubjectIds);
                return Accepted("students enroll subjects successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not enroll Subjects : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        
        
        
        
        [HttpDelete("subject/{SubjectId}")]
        public IActionResult DeleteSubject(Guid SubjectId)
        {
            try
            {
                var subject = _subjectRepo.GetSubjectById(SubjectId);
                if(subject == null)
                {
                    return NotFound($"Subject with id : { SubjectId} is not exists !");
                }
                _subjectRepo.DeleteSubject(SubjectId);
                return Accepted("subject is deleted successfuly !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not delete Subject : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Unrolls students in subjects.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/subjects/unroll
        ///     {
        ///         "studentIds": ["guid1", "guid2"],
        ///         "subjectIds": ["guid3", "guid4"]
        ///     }
        ///
        /// </remarks>
        /// <param name="unroll">The enrollment details.</param>
        /// <returns>Returns the result of the enrollment process.</returns>
        [HttpDelete("unroll")]
        public async Task<IActionResult> UnrollStudentsSubject([FromBody] Enroll unroll)
        {
            try
            {
                if (unroll.StudentIds == null || unroll.SubjectIds == null)
                    return BadRequest("students ids or subject id may be empty !");
                await _subjectRepo.StudentsUnrollSubject(unroll.StudentIds, unroll.SubjectIds);
                return Accepted("students unroll subjects successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not unroll Subject with students  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("subject/{subjectId}")]
        public IActionResult UpdateSubject(SubjectDto subject,Guid subjectId)
        {
            try
            {
                if (subject == null)
                    return BadRequest();
                var currentSubject = _subjectRepo.GetSubjectById(subjectId);
                if(currentSubject == null)
                {
                    return NotFound();
                }
                
                var subjectMap = _mapper.Map<Subject>(subject);
                 /*subjectMap.Id = subjectId;*/
                var result = _subjectRepo.UpdateSubject(subjectMap, subjectId);
                return Accepted(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not unroll Subject with students  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
