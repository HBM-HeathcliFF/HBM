using HBM.Persistence;

namespace HBM.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly HbmDbContext Context;

        protected TestCommandBase()
        {
            Context = HbmContextFactory.Create();
        }

        public void Dispose()
        {
            HbmContextFactory.Destroy(Context);
        }
    }
}