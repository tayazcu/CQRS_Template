using Microsoft.Extensions.DependencyInjection;
using Project.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.Extensions
{
    public static class CommitExtension
    {
        public static bool CommitCahnges(this IUnitOfWork unitOfWork)
        {
            int result = unitOfWork.Commit();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CommitCahnges2(IServiceProvider services)
        {
            IUnitOfWork uof = services.GetRequiredService<IUnitOfWork>();
            int result = uof.Commit();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
