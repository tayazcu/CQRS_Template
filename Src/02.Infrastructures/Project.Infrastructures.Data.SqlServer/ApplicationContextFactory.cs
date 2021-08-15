using Microsoft.EntityFrameworkCore;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructures.Data.SqlServer
{
    public class ApplicationContextFactory : DesignTimeApplicationContextFactoryBase<ApplicationContext>
    {
        public ApplicationContextFactory()
        { }

        protected override ApplicationContext CreateNewInstance(DbContextOptions<ApplicationContext> options)
        {
            return new ApplicationContext(options);
        }
    }
}
