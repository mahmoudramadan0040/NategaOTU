using AutoMapper;

namespace ControlOctoberTechnologyUniversitySystem.Models.DTO
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDto,Student>();
            CreateMap<SubjectDto,Subject>();
            CreateMap<DepartmentDto,Department>();
            CreateMap<StudentSubjectDto,StudentSubject>();
        }
    }
}
