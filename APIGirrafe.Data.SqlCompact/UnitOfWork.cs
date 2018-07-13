using Microsoft.EntityFrameworkCore;
using SoapUIClone.Data.Entities;

namespace SoapUIClone.Data.Sqlite
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<RequestHeader> RequestHeaders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=ApiTester.db");
        }

        public UnitOfWork(DbContextOptions options) 
            : base(options)
        {
            
        }
    }
}
