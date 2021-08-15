using Project.Core.Domain.Users.Entities;
using System.Threading;

namespace Project.Core.Contracts.Users.CommandRepositories
{
    public interface IUserInformationCommandRepository
    {
        void Add(UserInformation user);
        UserInformation Get(long userId);
        void Edit(UserInformation user);
    }
}
