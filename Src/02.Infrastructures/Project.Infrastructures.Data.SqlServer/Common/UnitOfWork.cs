using Microsoft.EntityFrameworkCore.Storage;
using Project.Framework;
using Project.Framework.DependencyInjection;
using Project.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Project.Infrastructures.Data.SqlServer.Common
{
    public class UnitOfWork : IUnitOfWork, IScopedDependency
    {
        private readonly WriteContext _context;
        public UnitOfWork(WriteContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }


    }
    public class ManageTransaction : IManageTransaction, IScopedDependency, IDisposable
    {
        IDbContextTransaction _transaction;
        private readonly WriteContext _context;
        public ManageTransaction(WriteContext context)
        {
            _context = context;
        }

        public void BeginTransaction() => _transaction = _context.Database.BeginTransaction();
        public void CommitTransaction() => _transaction.Commit();
        public void RollbackTransaction() => _transaction.Rollback();
        public bool TransactionIsOpen()
        {
            if (_transaction !=null)
            {
                return true;
            }
            return false;
        }

        public void Dispose() => _transaction.Dispose();
    }
}
