namespace Infrastructure.Persistence
{
    public class BaseContext : DbContext
    {
        #region Properties

        /// <summary>
        /// LoggerFactory for debbugs querys EF Core.
        /// </summary>
        public static readonly ILoggerFactory EfCoreLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information);
            builder.AddDebug();
        });

        #endregion

        #region Constructors

        /// <summary>
        /// BaseContext's protected Constructor.
        /// </summary>
        /// <param name="options">DbContextOptions object type</param>
        protected BaseContext(DbContextOptions options) : base(options)
        {
        }

        #endregion
    }
}
