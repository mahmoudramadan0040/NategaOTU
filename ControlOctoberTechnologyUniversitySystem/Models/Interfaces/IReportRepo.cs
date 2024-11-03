using Microsoft.AspNetCore.Mvc;

namespace ControlOctoberTechnologyUniversitySystem.Models.Interfaces
{
    public interface IReportRepo
    {
        public Task<List<object>> NategaReport(Guid departmentId, string studentStatus, [FromBody] Guid[] subjectIds);
    }
}
