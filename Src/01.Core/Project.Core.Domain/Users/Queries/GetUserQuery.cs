using Project.Framework.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Core.Domain.Users.Queries
{
    public class GetUserQuery : IQuery
    {
        public string UserId { get; set; }
    }
}
