using AutoMapper;
using HBM.Application.Common.Mappings;
using HBM.Application.Interfaces;
using HBM.Persistence;

namespace HBM.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public HbmDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = HbmContextFactory.Create();
            var configurationBuilder = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IHbmDbContext).Assembly));
            });
            Mapper = configurationBuilder.CreateMapper();
        }

        public void Dispose()
        {
            HbmContextFactory.Destroy(Context);
        }

        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
    }
}