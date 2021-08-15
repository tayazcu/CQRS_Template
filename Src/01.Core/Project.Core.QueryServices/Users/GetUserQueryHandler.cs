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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.QueryServices.Users
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserToShowVM>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IMapper _mapper;
        private readonly IUserReadonlyRepository _userQueryRepository;
        private readonly IUserInformationReadonlyRepository _userInformationQueryRepository;
        public GetUserQueryHandler(IResourceManager resourceManager, IMapper mapper,
                                   IUserInformationReadonlyRepository userInformationQueryRepository, IUserReadonlyRepository userQueryRepository)
        {
            _resourceManager = resourceManager;
            _mapper = mapper;
            _userQueryRepository = userQueryRepository;
            _userInformationQueryRepository = userInformationQueryRepository;
        }

        GetUserQuery query = null;
        User selectedUser = null;
        public UserToShowVM Handle(GetUserQuery query)
        {
            Assert.NotNull(query, nameof(query));
            this.query = query;

            IsValid();

            UserToShowVM userMapped = _mapper.Map<User, UserToShowVM>(selectedUser);
            UserInformation info = _userInformationQueryRepository.Get(selectedUser.Id);
            userMapped = _mapper.Map<UserInformation, UserToShowVM>(info, userMapped);

            return userMapped;
        }
        private void IsValid()
        {
            if (!query.UserId.HasValue())
                throw new NotFoundException(_resourceManager.GetName(SharedResource.NotFound, query.UserId));

            selectedUser = _userQueryRepository.Get(query.UserId);
            if (!selectedUser.IsExist())
                throw new NotFoundException(_resourceManager.GetName(SharedResource.NotFound, query.UserId));
        }
    }
}
