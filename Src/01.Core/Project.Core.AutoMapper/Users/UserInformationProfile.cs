using AutoMapper;
using Project.Core.Domain.Users.Commands;
using Project.Core.Domain.Users.Entities;
using Project.Core.ViewModels.Users.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.AutoMapper.Users
{
    public class UserInformationProfile : Profile
    {
        public UserInformationProfile()
        {
            CreateMap<AddUserCommand, UserInformation>();
            CreateMap<UserInformation, UserToShowVM>();
            CreateMap<EditUserCommand, UserInformation>();
        }
    }
}
