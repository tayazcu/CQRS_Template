using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructures.Data.SqlServer.Common
{
    public class ReadContext : ApplicationContext
    {
        public ReadContext(DbContextOptions<ApplicationContext> options) : base(options){}
    }

}
