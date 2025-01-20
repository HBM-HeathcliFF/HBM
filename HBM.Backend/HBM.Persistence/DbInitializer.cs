namespace HBM.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(HbmDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}