using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ControlOctoberTechnologyUniversitySystem.BusinessLogic
{
    public class ControlRole
    {
        public struct Grade
        {
            public  string Excuse = "عذر";
            public  string Cheating = "غش";
            public  string Absent = "غ";
            public  string E = "رل";
            public  string FF = "ض جـ";
            public  string F = "ض";
            public  string D = "ل";
            public  string C = "//جـ";
            public  string B = "جـ جـ";
            public  string A = "م";

            // general subject 
            public string Pass = "ناجح";
            public string Fail = "راسب";
            public Grade()
            {
            }
        }
        public struct Ratio
        {
            public  float E = 0.24F;
            public  float FF =0.40F;
            public  float F = 0.60F;
            public  float D = 0.65F;
            public  float C = 0.75F;
            public  float B = 0.85F;
            public  float A = 1;
            // general subject 
            public float Pass = 1F;
            public float Fail = .5F;
            public Ratio() { }
        }
        // this function show the student status like absent or cheating  or Excuse 
        public string StatusOther(string other)
        {
            try
            {
                Grade grade = new Grade();
                string result;
                if (other != null)
                {

                    return
                        result = other == grade.Excuse ? grade.Excuse :
                        other == grade.Cheating ? grade.Cheating :
                        other == grade.Absent ? grade.Absent : throw new Exception("Data is not defined !"); ;
                }
                else
                {
                    throw new Exception("Data is not defined !");
                }
            }catch(Exception e)
            {
                throw new Exception($"Data is not defined ! { e.Message }");
            }
            
        }

        // this function is calculate the grade of general subject that have fail or pass in exam 
        public string CalclateGeneralGrade(float MaxScore , float score)
        {
            try
            {
                Grade grade = new();
                Ratio ratio = new();
                string result;
                if (score != null)
                {
                    return 
                        result = score < (MaxScore * ratio.Fail) ? grade.Fail :
                        score > (MaxScore * ratio.Fail) && score <= (MaxScore * ratio.Pass) ? grade.Pass : throw new Exception("Data is not defined !"); ;
                   
                }
                else
                {
                    throw new Exception("Data is not defined !");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Data is not defined !{ex.Message}");
            }
        }
        // this function is calculate the grade of special subject 
        public string  CalculateGrade(float MaxScore ,float score  )
        {
            try
            {
                Grade grade = new();
                Ratio ratio = new();
                string result;
                if(score != null)
                {
                    result = score < (MaxScore * ratio.E) ? grade.E :
                    score > (MaxScore * ratio.E) && score < (MaxScore * ratio.FF) ? grade.FF :
                    score > (MaxScore * ratio.FF) && score < (MaxScore * ratio.F) ? grade.F :
                    score > (MaxScore * ratio.F) && score < (MaxScore * ratio.D) ? grade.D :
                    score > (MaxScore * ratio.D) && score < (MaxScore * ratio.C) ? grade.C :
                    score > (MaxScore * ratio.C) && score < (MaxScore * ratio.B) ? grade.B :
                    score > (MaxScore * ratio.B) && score <= (MaxScore * ratio.A) ? grade.A : throw new Exception("Data is not defined !"); ;
                    return result;
                }
                else
                {
                    throw new Exception("Data is not defined !");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Data is not defined !{ex.Message}");
            }

        }
    }
}
