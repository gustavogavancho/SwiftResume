using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public class ExperienciaRepository : Repository<Experiencia>, IExperienciaRepository
{
    public ExperienciaRepository(SwiftResumeDbContext context) : base(context) {}
}