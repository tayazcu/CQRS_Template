using AutoMapper;
using Project.Core.Domain.Users.Commands;
using Project.Core.ViewModels.Users.Command;

namespace Project.Core.AutoMapper.Users
{
    public class UserClaimProfile : Profile
    {
        public UserClaimProfile()
        {
            CreateMap<UserClaimToAddVM, AddClaimToUserCommand>();
            CreateMap<RemoveClaimFromUserVM, RemoveClaimFromUserCommand>();
        }
    }
}
