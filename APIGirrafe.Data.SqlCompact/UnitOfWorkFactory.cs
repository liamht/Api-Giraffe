using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SoapUIClone.Data.Sqlite
{
    public class UnitOfWorkFactory : IDesignTimeDbContextFactory<UnitOfWork>
    {
        public UnitOfWork CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UnitOfWork>();
            builder.UseSqlite(@"Data Source=ApiTester.db");

            return new UnitOfWork(builder.Options);
        }
    }
}
