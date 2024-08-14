using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TDD.Infrastructure;

namespace TDD.Application.Integration.Test
{
    public abstract class PersistantTest : IDisposable
    {
        private readonly TransactionScope _transactionScope;
        protected readonly ApplicationContext _context;
        public PersistantTest()
        {
            _transactionScope = new TransactionScope();
            _context = new ApplicationContext();
        }
        public void DetachedAllEntites()
        {
            var result = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added ||
               x.State == EntityState.Modified ||
               x.State == EntityState.Deleted).ToList();
            foreach (var item in result)
            {
                item.State = EntityState.Detached;
            }
        }
        public void Dispose()
        {
            _transactionScope.Dispose();
            _context.Dispose();
        }
    }
}
