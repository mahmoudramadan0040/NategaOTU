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
        public IActionResult GetNatega(Guid departmentId,string studentStatus, [FromQuery] Guid[]? subjectIds )
        {
            try
            {
                var result =  _reportRepo.NategaReport(departmentId, studentStatus, subjectIds).Result;
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



        [HttpGet("/subject-report/{departmentId}/{studentStatus}")]
        public IActionResult GetReportofSubjectsGrades(Guid departmentId, string studentStatus,[FromQuery] Guid[]? subjectIds)
        {
            try
            {
                var result =  _reportRepo.SubjectsReports(departmentId, studentStatus, subjectIds).Result;
                if (result == null )
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"can not get subjects report  : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
