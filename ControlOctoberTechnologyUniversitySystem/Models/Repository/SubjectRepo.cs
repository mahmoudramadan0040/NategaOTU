namespace ControlOctoberTechnologyUniversitySystem.Models.Repository
{
    public class SubjectRepo
    {
        public readonly ControlDbContext _context;
        public SubjectRepo(ControlDbContext context)
        {
            _context = context;
        }
    }
}
