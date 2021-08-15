using Microsoft.EntityFrameworkCore;
using Project.Framework.Extensions;

namespace Project.Infrastructures.Data.SqlServer.Common
{
    public class WriteContext : ApplicationContext
    {
        public WriteContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //public virtual void Detach<T>(T entity) where T : class
        //public virtual void Detach(dynamic entity)
        //{
        //    Assert.NotNull(entity, nameof(entity));
        //    var entry = this.Entry(entity);
        //    if (entry != null)
        //        entry.State = EntityState.Detached;
        //}
        //public virtual void Attach(dynamic entity)
        //{
        //    Assert.NotNull(entity, nameof(entity));
        //    if (this.Entry(entity).State == EntityState.Detached)
        //        this.Attach(entity);
        //}
    }

}
