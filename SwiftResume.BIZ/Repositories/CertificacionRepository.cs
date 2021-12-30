using SwiftResume.COMMON.Models;
using SwiftResume.DAL.EFCORE;
using SwiftResume.DAL.EFCORE.Services;

namespace SwiftResume.BIZ.Repositories;

public class CertificacionRepository : Repository<Certificacion>, ICertificacionRepository
{
    public CertificacionRepository(SwiftResumeDbContext context) : base(context) {}
}