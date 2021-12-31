using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public class EducacionRepository : Repository<Educacion>, IEducacionRepository
{
    public EducacionRepository(SwiftResumeDbContext context) : base(context) {}
}