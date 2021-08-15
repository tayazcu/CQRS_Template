using Project.Framework.Queries;

namespace Project.Core.Domain.Users.Queries
{
    public class GetUserRolesQuery : IQuery
    {
        public string UserId { get; set; }
    }
}
