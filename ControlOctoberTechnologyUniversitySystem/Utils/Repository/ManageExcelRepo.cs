using ControlOctoberTechnologyUniversitySystem.Utils.Interfaces;
using OfficeOpenXml;
using NPOI.XSSF.UserModel; // For .xlsx files
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using ControlOctoberTechnologyUniversitySystem.Models;
namespace ControlOctoberTechnologyUniversitySystem.Utils.Repository
{
    public class ManageExcelRepo : IManageExcelFiles
    {
       
        public void ImportStudentDataFromExcel(IFormFile file)
        {
            var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0;
            try
            {
                IWorkbook workbook;
                if (Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                {
                    // for file that extension is xlsx 
                    workbook = new XSSFWorkbook(stream);

                }else
                {
                    // for file that extension is xls 
                    stream.Position = 0;
                    workbook = new HSSFWorkbook(stream);
                }
                ISheet sheet = workbook.GetSheetAt(0); // Assuming the first sheet
                var data = new List<Student>();

                foreach(IRow excelRow in sheet)
                {
                    if (excelRow.RowNum == 0) // Skip the header row
                        continue;
                    if (excelRow != null) // null is when the row only contains empty cells
                    {
                        var student_id = excelRow.GetCell(0).ToString().Trim() != null ?
                            excelRow.GetCell(0)?.ToString().Trim() : 
                            throw new Exception($"Data in Excel may have null value ! in row ${excelRow.RowNum}");


                        var fullname = excelRow.GetCell(1).ToString().Trim() != null ?
                            excelRow.GetCell(1)?.ToString().Trim() : 
                            throw new Exception($"Data in Excel may have null value ! in row ${excelRow.RowNum}");
                        
                        
                        data.Add(new Student
                        {
                            student_id = student_id,
                            fullname = fullname
                        });
                    }
                }


            }
            catch(Exception ex)
            {
               

                
            }


        }
        public void ExportStudentDataToExcel()
        {
            
           
        }

        

        public bool isEmptyFile(IFormFile file)
        {
            return file == null || file.Length <= 0 ? true : false;
        }

        public bool isExcelFile(IFormFile file)
        {
            return Path.GetExtension(file.FileName).ToLower() != ".xlsx" &&
                Path.GetExtension(file.FileName).ToLower() != ".xls" ? false : true;
        }
    }
}
