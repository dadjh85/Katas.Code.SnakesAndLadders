namespace Test.Unit.GlobalConfiguration
{
    public static class ContextConfig
    {
        public static TContext ContextConfigure<TContext>(string databaseName = "TestDatabase") where TContext : DbContext
        {
            DbContextOptions<TContext> options = new DbContextOptionsBuilder<TContext>().EnableSensitiveDataLogging().UseInMemoryDatabase(databaseName)
                                                                                        .Options;

            return (TContext)Activator.CreateInstance(typeof(TContext), options);
        }

        public static async Task StartDatabase<TContext>(TContext seedContext) where TContext : DbContext
        {
            if (seedContext != null)
            {
                await seedContext.Database.EnsureDeletedAsync();
                await seedContext.Database.EnsureCreatedAsync();
                await seedContext.SaveChangesAsync();
            }
        }
    }
}
