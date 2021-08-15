using AutoMapper;
using Project.Core.Contracts.Users.ReadonlyRepositories;
using Project.Core.Domain.Users.Entities;
using Project.Core.Domain.Users.Queries;
using Project.Core.Infrastructures.Identity;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users;
using Project.Core.ViewModels.Users.Query;
using Project.Framework.DependencyInjection;
using Project.Framework.Domain;
using Project.Framework.Exceptions;
using Project.Framework.Extensions;
using Project.Framework.Queries;
using Project.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Core.QueryServices.Users
{
    public class GetAllSystemRoleQueryHandler : IQueryHandler<GetAllSystemRoleQuery, IEnumerable<string>>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IMapper _mapper;
        private readonly IUserReadonlyRepository _userQueryRepository;
        private readonly IUserInformationReadonlyRepository _userInformationQueryRepository;
        public GetAllSystemRoleQueryHandler(IResourceManager resourceManager, IMapper mapper,
                                   IUserInformationReadonlyRepository userInformationQueryRepository, IUserReadonlyRepository userQueryRepository)
        {
            _resourceManager = resourceManager;
            _mapper = mapper;
            _userQueryRepository = userQueryRepository;
            _userInformationQueryRepository = userInformationQueryRepository;
        }

        public IEnumerable<string> Handle(GetAllSystemRoleQuery query)
        {
            return EnumExtension.ConvertToStringList<Roles>();
        }
       
    }
}
