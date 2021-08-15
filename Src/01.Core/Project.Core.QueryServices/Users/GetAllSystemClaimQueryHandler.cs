using AutoMapper;
using Project.Core.Contracts.Users.ReadonlyRepositories;
using Project.Core.Domain.Users.Queries;
using Project.Core.Infrastructures.Identity;
using Project.Framework.DependencyInjection;
using Project.Framework.Extensions;
using Project.Framework.Queries;
using Project.Framework.Resources;
using System.Collections.Generic;

namespace Project.Core.QueryServices.Users
{
    public class GetAllSystemClaimQueryHandler : IQueryHandler<GetAllSystemClaimQuery, IEnumerable<string>>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IMapper _mapper;
        private readonly IUserReadonlyRepository _userQueryRepository;
        private readonly IUserInformationReadonlyRepository _userInformationQueryRepository;
        public GetAllSystemClaimQueryHandler(IResourceManager resourceManager, IMapper mapper,
                                   IUserInformationReadonlyRepository userInformationQueryRepository, IUserReadonlyRepository userQueryRepository)
        {
            _resourceManager = resourceManager;
            _mapper = mapper;
            _userQueryRepository = userQueryRepository;
            _userInformationQueryRepository = userInformationQueryRepository;
        }

        public IEnumerable<string> Handle(GetAllSystemClaimQuery query)
        {
            return EnumExtension.ConvertToStringList<Claims>();
        }

    }
}
