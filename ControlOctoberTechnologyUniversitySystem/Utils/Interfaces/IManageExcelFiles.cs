namespace ControlOctoberTechnologyUniversitySystem.Utils.Interfaces
{
    public interface IManageExcelFiles
    {
        public bool isExcelFile(IFormFile file);
        public bool isEmptyFile(IFormFile file);

        public void ImportStudentDataFromExcel(IFormFile file);
        public void ExportStudentDataToExcel();
        
    }
}
