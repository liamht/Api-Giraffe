using Microsoft.EntityFrameworkCore;
using APIGirrafe.Data.Entities;
using System.Threading.Tasks;

namespace APIGirrafe.Data.UnitOfWork
{
    public class SqliteUnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestGroup> RequestGroups { get; set; }

        public DbSet<Header> Headers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=ApiGiraffe.db");
        }

        void IUnitOfWork.SaveChanges()
        {
            SaveChanges();
        }

        async Task IUnitOfWork.SaveChangesAsync()
        {
            await SaveChangesAsync();
        }

        public SqliteUnitOfWork(DbContextOptions options) 
            : base(options)
        {
            
        }
    }
}
