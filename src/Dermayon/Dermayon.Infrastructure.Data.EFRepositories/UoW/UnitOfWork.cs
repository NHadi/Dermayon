using Dermayon.Infrastructure.Data.EFRepositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.Data.EFRepositories.UoW
{
    public class UnitOfWork<Tcontext> : IUnitOfWork<Tcontext> where Tcontext : DbContext
    {
        public Tcontext Context { get; }

        public UnitOfWork(Tcontext context) => Context = context;
        public void Commit()
        => Context.SaveChanges();

        public void Dispose()
         => Context.Dispose();

        public async Task CommitAsync()
            => await Context.SaveChangesAsync();

        public void BeginTransaction()
        => Context.Database.BeginTransaction();
        public void CommitTransaction()
        => Context.Database.CommitTransaction();
        public void RollbackTransaction()
        => Context.Database.RollbackTransaction();
        public void DisposeTransaction()
            => Context.Database.CurrentTransaction.Dispose();
    }
}
