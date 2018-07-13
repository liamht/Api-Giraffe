using Microsoft.EntityFrameworkCore;
using APIGirrafe.Data.Entities;

namespace APIGirrafe.Data.UnitOfWork
{
    public class SqliteUnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestGroup> RequestGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=ApiTester.db");
        }

        void IUnitOfWork.SaveChanges()
        {
            SaveChanges();
        }

        public SqliteUnitOfWork(DbContextOptions options) 
            : base(options)
        {
            
        }
    }
}
