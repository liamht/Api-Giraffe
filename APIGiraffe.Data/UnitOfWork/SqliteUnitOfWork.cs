using Microsoft.EntityFrameworkCore;
using APIGiraffe.Data.Entities;
using System.Threading.Tasks;

namespace APIGiraffe.Data.UnitOfWork
{
    public class SqliteUnitOfWork : DbContext, IUnitOfWork
    {
        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<RequestGroup> RequestGroups { get; set; }

        public virtual DbSet<Header> Headers { get; set; }

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
