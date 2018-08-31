using Microsoft.EntityFrameworkCore;
using OMX.Data;
using System;

namespace OMX.Test.Mocks
{
    public class MockDbContext
    {
        public static OmxDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<OmxDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new OmxDbContext(options);

            return dbContext;

        }
    }
}
