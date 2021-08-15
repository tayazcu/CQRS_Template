using Project.Framework.Queries;

namespace Project.Core.Domain.Users.Queries
{
    public class GetUserClaimsQuery : IQuery
    {
        public string UserId { get; set; }
    }
}
