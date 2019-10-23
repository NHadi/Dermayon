using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Repositories.MongoDb.UoW
{
    public class UnitOfWorkMongo<Tcontext> : IUnitOfWorkMongo<Tcontext> 
        where Tcontext : MongoContext
    {
        public Tcontext Context { get; }
        public UnitOfWorkMongo(Tcontext context) => Context = context;
        public async Task<bool> Commit()
        {
            var changeAmount = await Context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        => Context.Dispose();
        
    }
}
