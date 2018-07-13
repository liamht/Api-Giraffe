using Microsoft.EntityFrameworkCore;
using APIGirrafe.Data.Entities;

namespace APIGirrafe.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbSet<RequestGroup> RequestGroups { get; set; }
        DbSet<Request> Requests { get; set; }
        void SaveChanges();
    }
}
