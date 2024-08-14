using AutoMapper;
using ControlOctoberTechnologyUniversitySystem.Models;
using ControlOctoberTechnologyUniversitySystem.Models.DTO;
using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using ControlOctoberTechnologyUniversitySystem.Utils;

using Microsoft.AspNetCore.Mvc;



namespace ControlOctoberTechnologyUniversitySystem.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        
        public class StudentFilter
        {
            
            public bool? graduated;
            
            public string? StudentConstraint;
    
            public string? StudentStatus;
        }
        private readonly IStudentRepo _studentRepo;
        private readonly ILogger<StudentController> _logger;
        private readonly IManageImageRepo _manageImageRepo;
        private readonly IMapper _mapper;
        public StudentController(IStudentRepo studentRepo,
            ILogger<StudentController> logger,
            IManageImageRepo manageImageRepo,
            IMapper mapper)
        {
            _studentRepo = studentRepo;
            _logger = logger;
            _manageImageRepo = manageImageRepo;
            _mapper = mapper;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportStudentData(IFormFile file)
        {
            try
            {
                 var result = await _studentRepo.ImportStudentData(file);
                return Ok(result);

            }catch(Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet]
        public ActionResult<Student> GetAllstudent()
        {
            try
            {
                var students = _studentRepo.GetStudents();
                if(students == null)
                {
                    return NotFound();
                }
                return Ok(students);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>
        /// Enrolls students in subjects.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/students/filter
        ///     params [
        ///         graduated,
        ///         StudentConstraint,
        ///         StudentStatus
        ///     ]
        ///
        /// </remarks>
        /// <param name="enroll">The enrollment details.</param>
        /// <returns>Returns the result of the enrollment process.</returns>
        [HttpGet("filter")]
        [ProducesResponseType(201)]
        public async  Task<ActionResult<IEnumerable<Student>>> GetFilterStudent(
            [FromQuery] StudentFilter filter) {
            try
            {
                // without filter its return all student in system 
                if(filter is null)
                {
                    var students = _studentRepo.GetStudents();
                    if (students == null)
                    {
                        return NotFound();
                    }
                    return Ok(students);
                }

                var result  = await _studentRepo.GetFilterStudentsAsync(filter);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("student/{studentId}")]
        public ActionResult<Student> Getstudent(Guid studentId)
        {
            try
            {
                var student = _studentRepo.GetStudentById(studentId);
                if (student == null)
                    return NotFound($"student with id : {studentId} , hasn't been found in db !");
                
                return Ok(student);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("student")]
        public async Task<ActionResult<Student>> Create([FromForm] StudentDto student)
        {
            try
            {
                if(student == null)
                {
                    return BadRequest("student object is null ");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client ! ");
                    return BadRequest("Invalid student object !");
                }
                var studentMap = _mapper.Map<Student>(student);

                if (student.StudentImage != null)
                {
                    // check the files is images or not
                    foreach (var img in student.StudentImage)
                    {
                        if (!img.IsImage())
                            return BadRequest($"this file is not image ${img.FileName}");
                    }

                    List<StudentImage> StudentImages = new List<StudentImage>();
                    studentMap.StudentImages = new List<StudentImage>();
                    foreach (var img in student.StudentImage)
                    {
                        var url = await _manageImageRepo.AddImage(img);
                        StudentImage image = new StudentImage
                        {
                            ImageUrl = url
                        };
                        studentMap.StudentImages.Add(image);
                    }
                }
                
                
                
                
                var result = _studentRepo.CreateStudent(studentMap);
                return Ok(result);


            }
            catch (Exception ex)
            {
                _logger.LogError("Can not create student : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("student/{studentId}")]
        public IActionResult DeleteStudent(Guid studentId)
        {
            try
            {
                var student = _studentRepo.GetStudentById(studentId);
                if (student == null)
                    return NotFound($"student with id : {studentId} , hasn't been found in db !");
                _studentRepo.DeleteStudent(studentId);
                return Accepted("student is deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        




    }
}
