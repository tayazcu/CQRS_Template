using Project.Core.Contracts.Users.CommandRepositories;
using Project.Core.Contracts.Users.ReadonlyRepositories;
using Project.Core.Domain.Users.Entities;
using Project.Framework.DependencyInjection;
using Project.Framework.Domain;
using Project.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.CommandServices.Base
{
    public class UpdateSecurityStampService : IScopedDependencySingle
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadonlyRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        public UpdateSecurityStampService( IUnitOfWork unitOfWork, IUserReadonlyRepository userQueryRepository,IUserCommandRepository userCommandRepository) 
        {
            _userCommandRepository = userCommandRepository;
            _unitOfWork = unitOfWork;
            _userQueryRepository = userQueryRepository;
        }

        public void Update(User user)
        {
            string securityStamp = Guid.NewGuid().ToString();
            _userCommandRepository.UpdateSecurityStamp(user, securityStamp);
            _userQueryRepository.UpdateSecurityStamp(user, securityStamp);
            bool result= _unitOfWork.CommitCahnges();
        }
    }
}
