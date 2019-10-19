using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data
{
    public interface ITransactionAble
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void DisposeTransaction();
    }
}
