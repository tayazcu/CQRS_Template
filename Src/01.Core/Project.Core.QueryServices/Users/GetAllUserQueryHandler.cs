using AutoMapper;
using Project.Core.Contracts.Users.ReadonlyRepositories;
using Project.Core.Domain.Users.Entities;
using Project.Core.Domain.Users.Queries;
using Project.Core.Resources.Resources;
using Project.Core.ViewModels.Users.Query;
using Project.Framework.DependencyInjection;
using Project.Framework.Exceptions;
using Project.Framework.Extensions;
using Project.Framework.Queries;
using Project.Framework.Resources;
using System.Collections.Generic;

namespace Project.Core.QueryServices.Users
{
    public class GetAllUserQueryHandler : IQueryHandler<GetAllUserQuery, IEnumerable<UserToShowVM>>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IMapper _mapper;
        private readonly IUserReadonlyRepository _userQueryRepository;
        public GetAllUserQueryHandler(IResourceManager resourceManager, IMapper mapper,IUserReadonlyRepository userQueryRepository)
        {
            _resourceManager = resourceManager;
            _mapper = mapper;
            _userQueryRepository = userQueryRepository;
        }

        public IEnumerable<UserToShowVM> Handle(GetAllUserQuery query)
        {
            return _userQueryRepository.GetAll();
        }
    }
}
