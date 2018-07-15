using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace APIGiraffe.Data.UnitOfWork
{
    public class UnitOfWorkFactory : IDesignTimeDbContextFactory<SqliteUnitOfWork>
    {
        public SqliteUnitOfWork CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SqliteUnitOfWork>();
            builder.UseSqlite(@"Data Source=APIGiraffe.db");

            return new SqliteUnitOfWork(builder.Options);
        }
    }
}
