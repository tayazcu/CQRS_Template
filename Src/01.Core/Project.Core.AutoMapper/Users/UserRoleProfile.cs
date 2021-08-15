using AutoMapper;
using Project.Core.Domain.Users.Commands;
using Project.Core.ViewModels.Users.Command;

namespace Project.Core.AutoMapper.Users
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRoleToAddVM, AddRoleToUserCommand>();
            CreateMap<RemoveRoleFromUserVM, RemoveRoleFromUserCommand>();
            CreateMap<UserRoleStatusToEditVM, ChangeStatusUserRoleCommand>();
        }
    }
}
