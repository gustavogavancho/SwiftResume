using Microsoft.EntityFrameworkCore;
using System;

namespace SwiftResume.DAL.EFCORE
{
    public class SwiftResumeDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public SwiftResumeDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public SwiftResumeDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SwiftResumeDbContext>();

            _configureDbContext(options);

            return new SwiftResumeDbContext(options.Options);
        }
    }
}
