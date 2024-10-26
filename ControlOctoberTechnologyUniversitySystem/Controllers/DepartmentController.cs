using AutoMapper;
using ControlOctoberTechnologyUniversitySystem.Models;
using ControlOctoberTechnologyUniversitySystem.Models.DTO;
using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlOctoberTechnologyUniversitySystem.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // get all department  [ done ]
        // get department by id [ done ]
        // create department [ done ] 
        // add students to dpeartment [ done ]
        // add subjects to department [ done ]

        // delete department [ done ]
        // get all student in department [ done ]
        // get all subject in department [ done ]
        // update department 
        
        private readonly IDepartmentRepo _departmentRepo;
        private ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;

        public DepartmentController(
            IDepartmentRepo departmentRepo,
            ILogger<DepartmentController> logger,
            IMapper mapper
            ) { 
            _departmentRepo = departmentRepo;
            _logger= logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetDepartments()
        {
            try
            {
                var depatments = _departmentRepo.GetDepartments(); 
                if(depatments == null)
                    return NotFound();
                return Ok(depatments);

            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get category  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("report")]
        public async Task<ActionResult<List<DepartmentReportDto>>> GetDeaprtmentsReports()
        {
            try
            {
                var depatments = await _departmentRepo.GetAllDepartmentsWithStatistics();
                if (depatments == null)
                    return NotFound();
                return Ok(depatments);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get category  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("department/{departmentId}")]
        public IActionResult GetDepartment(Guid departmentId)
        {
            try
            {
                var department = _departmentRepo.GetDepartment(departmentId);
                if(department==null)
                    return NotFound();
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get category  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("department/{DepartmentId}/students")]
        public IActionResult GetStudentsDepartment(Guid DepartmentId)
        {
            try
            {
                var students = _departmentRepo.GetStudentsByDepatment(DepartmentId);
                if (students == null)
                    return NotFound($"this department with id {DepartmentId} not have any students");
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get students  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("department/{DepartmentId}/subjects")]
        public IActionResult GetSubjectsDepartment(Guid DepartmentId)
        {
            try
            {
                var subjects = _departmentRepo.GetSubjectsByDepartment(DepartmentId);
                if (subjects == null)
                    return NotFound($"this department with id {DepartmentId} not have any subjects");
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get subjects  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("department")]
        public IActionResult CreateDepartment(DepartmentDto department)
        {
            
            try
            {
                if (department == null)
                    return BadRequest();
                var departmentMap = _mapper.Map<Department>(department);
                var result = _departmentRepo.CreateDepartment(departmentMap);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not create  department  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("department/{departmentId}/subjects")]
        public IActionResult AddSubjectsToDepartment(Guid departmentId , [FromBody] Guid[] subjectIds)
        {
            try
            {
                var department = _departmentRepo.GetDepartment(departmentId);
                if (department == null)
                    return NotFound();
                _departmentRepo.addSubjectToDepartment(departmentId, subjectIds);
                return Accepted("Subjects is  Added successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not add  subject to department  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        
        [HttpPost("department/{departmentId}/students")]
        public IActionResult AddStudentsToDepartment(Guid departmentId, [FromBody] Guid [] studentIds)
        {
            try
            {
                var department = _departmentRepo.GetDepartment(departmentId);
                if (department == null)
                    return NotFound();
                _departmentRepo.enrollStudentsInDepartment(departmentId, studentIds);
                
                return Accepted("students is  Added successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not add  students to department  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        

        [HttpDelete("department/{departmentId}")]
        public IActionResult DeleteDepartment(Guid departmentId)
        {
            try
            {
                var department = _departmentRepo.GetDepartment(departmentId);
                if (department == null)
                    return NotFound();
                _departmentRepo.DeleteDepartment(department);
                return Accepted("department deleted successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not delete  department  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("department/{departmentId}/students")]
        public IActionResult DeleteStudentFromDepartment(Guid departmentId, [FromBody] Guid[] studentIds)
        {
            try
            {
                var department = _departmentRepo.GetDepartment(departmentId);
                if (department == null)
                    return NotFound();
                _departmentRepo.UnrollStudentsFromDepartment(departmentId, studentIds);
                return Accepted("student unroll the department successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not delete  student form department  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("department/{departmentId}/subject")]
        public IActionResult DeleteSubjectFromDepartment(Guid departmentId, [FromBody] Guid subjectIds)
        {
            try
            {
                var department = _departmentRepo.GetDepartment(departmentId);
                if (department == null)
                    return NotFound();
                _departmentRepo.removeSubjectFromDepartment(departmentId, subjectIds);
                return Accepted("Subject unroll the department successfully !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not unroll  subject form department  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("department/{departmentId}")]
        public IActionResult UpdateDepartment(DepartmentDto department, Guid departmentId)
        {
            try
            {
                if (department == null)
                    return BadRequest();
                var currentDepartment = _departmentRepo.GetDepartment(departmentId);
                if (currentDepartment == null)
                {
                    return NotFound();
                }

                var DepartmentMap = _mapper.Map<Department>(department);
                var result = _departmentRepo.UpdateDepartment(DepartmentMap, departmentId);
                return Accepted(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not delete department with : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }



    }
}
