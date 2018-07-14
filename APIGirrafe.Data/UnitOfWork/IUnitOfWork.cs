using Microsoft.EntityFrameworkCore;
using APIGirrafe.Data.Entities;
using System.Threading.Tasks;

namespace APIGirrafe.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbSet<RequestGroup> RequestGroups { get; set; }

        DbSet<Request> Requests { get; set; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
