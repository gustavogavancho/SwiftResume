using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public class ProyectoRepository : Repository<Proyecto>, IProyectoRepository
{
    public ProyectoRepository(SwiftResumeDbContext context) : base(context) {}
}
