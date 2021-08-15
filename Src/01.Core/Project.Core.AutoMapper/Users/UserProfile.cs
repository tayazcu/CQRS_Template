using AutoMapper;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Entities;
using Project.Core.Domain.Users.Queries;
using Project.Core.ViewModels.Users.Command;
using Project.Core.ViewModels.Users.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.AutoMapper.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserToAddVM, AddUserCommand>()
                .ForMember(
                    dest => dest.RegisterDate,
                    opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<User, UserToShowVM>();

            CreateMap<LoginVM, LoginQuery>();
            CreateMap<UserToEditVM, EditUserCommand>();
            CreateMap<EditUserCommand, User>();
        }
    }
}
