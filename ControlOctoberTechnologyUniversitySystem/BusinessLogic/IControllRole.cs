

namespace ControlOctoberTechnologyUniversitySystem.BusinessLogic
{
    public interface IControllRole
    {
        public string StatusOther(string other);
        public string CalclateGeneralGrade(float MaxScore, float FinalScore, float SemesterScore = 0);
        public string CalculateGrade(float MaxScore, float FinalScore, float SemesterScore = 0);
        
    }
}
