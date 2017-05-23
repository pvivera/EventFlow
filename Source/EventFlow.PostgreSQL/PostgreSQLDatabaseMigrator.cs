#if NET451

using EventFlow.Logs;

namespace EventFlow.PostgreSQL
{
    public class PostgreSQLDatabaseMigrator : EventFlow.Sql.Migrations.PostgreSQLDatabaseMigrator<IPostgreSQLConfiguration>, IPostgreSQLDatabaseMigrator
    {
        public PostgreSQLDatabaseMigrator(
            ILog log,
            IPostgreSQLConfiguration sqlConfiguration)
            : base(log, sqlConfiguration)
        {
        }
    }
}
#endif