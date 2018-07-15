using Microsoft.EntityFrameworkCore;
using APIGiraffe.Data.Entities;
using System.Threading.Tasks;

namespace APIGiraffe.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbSet<RequestGroup> RequestGroups { get; set; }

        DbSet<Request> Requests { get; set; }

        DbSet<Header> Headers { get; set; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
