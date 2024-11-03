using AutoMapper;
using ControlOctoberTechnologyUniversitySystem.Models.Interfaces;
using ControlOctoberTechnologyUniversitySystem.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlOctoberTechnologyUniversitySystem.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IMapper _mapper;
        private readonly IReportRepo _reportRepo;
        public ReportController(ILogger<ReportController> logger, IMapper mapper, IReportRepo reportRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _reportRepo = reportRepo;
        }

        [HttpGet("/natega/{departmentId}/{studentStatus}")]
        public IActionResult GetNatega(Guid departmentId,string studentStatus, [FromBody] Guid[]? subjectIds )
        {
            try
            {
                var result = _reportRepo.NategaReport(departmentId, studentStatus, subjectIds);
                if (result == null )
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get report of natega   : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        /*[HttpGet("/subject-report/{subjectId}/{studentStatus}")]
        public IActionResult GetSubjectGrades(Guid departmentId, string studentStatus)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get category  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }*/

    }
}
