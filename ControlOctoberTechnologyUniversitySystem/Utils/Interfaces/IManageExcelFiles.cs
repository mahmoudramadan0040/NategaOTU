using ControlOctoberTechnologyUniversitySystem.Models;

namespace ControlOctoberTechnologyUniversitySystem.Utils.Interfaces
{
    public interface IManageExcelFiles
    {
        public bool isExcelFile(IFormFile file);
        public bool isEmptyFile(IFormFile file);

        public List<Student> ImportStudentDataFromExcel(IFormFile file);
        public void ImportStudentSubjectFromExcel(IFormFile file,Subject subject);

        public void ExportStudentDataToExcel();
        
    }
}
