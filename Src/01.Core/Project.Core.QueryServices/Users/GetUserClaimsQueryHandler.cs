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
    public class GetUserClaimsQueryHandler : IQueryHandler<GetUserClaimsQuery, IEnumerable<string>>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IMapper _mapper;
        private readonly IUserReadonlyRepository _userQueryRepository;
        private readonly IIdentityReadonlyRepository _identityQueryRepository;
        public GetUserClaimsQueryHandler(IResourceManager resourceManager, IMapper mapper,
                                         IUserReadonlyRepository userQueryRepository , IIdentityReadonlyRepository identityQueryRepository)
        {
            _resourceManager = resourceManager;
            _mapper = mapper;
            _userQueryRepository = userQueryRepository;
            _identityQueryRepository = identityQueryRepository;
        }

        GetUserClaimsQuery query = null;
        User selectedUser = null;
        public IEnumerable<string> Handle(GetUserClaimsQuery query)
        {
            Assert.NotNull(query, nameof(query));
            this.query = query;

            IsValid();

            return _identityQueryRepository.GetAllUserClaim(selectedUser.Id);
        }
        private void IsValid()
        {
            if (!query.UserId.HasValue())
                throw new NotFoundException(_resourceManager.GetName(SharedResource.NotFound , query.UserId));

            selectedUser = _userQueryRepository.Get(query.UserId);
            if (!selectedUser.IsExist())
                throw new NotFoundException(_resourceManager.GetName(SharedResource.NotFound, query.UserId));
        }
    }
}
