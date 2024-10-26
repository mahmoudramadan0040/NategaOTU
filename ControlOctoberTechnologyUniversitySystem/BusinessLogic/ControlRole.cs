using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ControlOctoberTechnologyUniversitySystem.BusinessLogic
{
    public class ControlRole: IControllRole
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
            public float FailFinal = .4F;
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
        public string CalclateGeneralGrade(float MaxScore, float FinalScore, float SemesterScore = 0)
        {
            try
            {
                Grade grade = new();
                Ratio ratio = new();
                float totalScore = FinalScore + SemesterScore;
                string result;
                
                return 
                    result = FinalScore < (MaxScore * ratio.FailFinal) ? grade.Fail :
                    totalScore > (MaxScore * ratio.Fail) && totalScore <= (MaxScore * ratio.Pass) ? grade.Pass : throw new Exception("Data is not defined !"); ;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Data is not defined !{ex.Message}");
            }
        }
        // this function is calculate the grade of special subject 
        public string  CalculateGrade(float MaxScore ,float FinalScore,float SemesterScore = 0 )
        {
            try
            {
                Grade grade = new();
                Ratio ratio = new();
                float totalScore = FinalScore + SemesterScore;
                string result;
                
                result = FinalScore < (MaxScore * ratio.E) ? grade.E :
                totalScore > (MaxScore * ratio.E) && totalScore < (MaxScore * ratio.FF) ? grade.FF :
                totalScore > (MaxScore * ratio.FF) && totalScore < (MaxScore * ratio.F) ? grade.F :
                totalScore > (MaxScore * ratio.F) && totalScore < (MaxScore * ratio.D) ? grade.D :
                totalScore > (MaxScore * ratio.D) && totalScore < (MaxScore * ratio.C) ? grade.C :
                totalScore > (MaxScore * ratio.C) && totalScore < (MaxScore * ratio.B) ? grade.B :
                totalScore > (MaxScore * ratio.B) && totalScore <= (MaxScore * ratio.A) ? grade.A : throw new Exception("Data is not defined !"); ;
                return result;       
            }
            catch (Exception ex)
            {
                throw new Exception($"Data is not defined !{ex.Message}");
            }

        }
    }
}
