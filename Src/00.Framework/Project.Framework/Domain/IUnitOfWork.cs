using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.Domain
{
    public interface IUnitOfWork
    {
        int Commit();
    }
    public interface IManageTransaction
    {
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        bool TransactionIsOpen();
    }
}
