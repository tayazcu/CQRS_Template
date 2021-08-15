using Project.Core.Domain.Users.Entities;

namespace Project.Core.Contracts.Users.ReadonlyRepositories
{
    public interface IUserInformationReadonlyRepository
    {
        UserInformation Get(long userId);
    }
}
