using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Contracts.Users.ReadonlyRepositories;
using Project.Core.Domain.Users.Dtos;
using Project.Core.Domain.Users.Entities;
using Project.Core.Domain.Users.Queries;
using Project.Core.Resources.Resources;
using Project.Framework;
using Project.Framework.DependencyInjection;
using Project.Framework.Exceptions;
using Project.Framework.Extensions;
using Project.Framework.Queries;
using Project.Framework.Resources;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.QueryServices.Users
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, AccessToken>
    {
        private readonly IResourceManager _resourceManager;
        private readonly IMapper _mapper;
        private readonly IUserReadonlyRepository _userQueryRepository;
        private readonly SiteSettings _siteSetting;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserInformationReadonlyRepository _userInformationQueryRepository;

        public LoginQueryHandler(IResourceManager resourceManager, IMapper mapper, IUserReadonlyRepository userQueryRepository,
                                 IOptionsSnapshot<SiteSettings> settings, SignInManager<User> signInManager , IUserInformationReadonlyRepository userInformationQueryRepository)
        {
            _resourceManager = resourceManager;
            _mapper = mapper;
            _userQueryRepository = userQueryRepository;
            _siteSetting = settings.Value;
            _signInManager = signInManager;
            _userInformationQueryRepository = userInformationQueryRepository;
        }

        LoginQuery query = null;
        User selectedUser = null;
        public AccessToken Handle(LoginQuery query)
        {
            Assert.NotNull(query, nameof(query));
            this.query = query;

            selectedUser = _userQueryRepository.Get(query.username , true);
            IsValid();
            AccessToken token = GenerateAsync().Result;
            return token;
        }
        public async Task<AccessToken> GenerateAsync()
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey); 
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.Encryptkey); 
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await _getClaimsAsync();

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.JwtSettings.Issuer,
                Audience = _siteSetting.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                //EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims),

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            return new AccessToken(securityToken);
        }
        private async Task<IEnumerable<Claim>> _getClaimsAsync()
        {
            UserInformation userInfo = _userInformationQueryRepository.Get(selectedUser.Id) ;
            var result = await _signInManager.ClaimsFactory.CreateAsync(selectedUser);

            var list = new List<Claim>(result.Claims);
            list.Add(new Claim("FullName", userInfo.EnglishFirstName + userInfo.EnglishLastName));

            return list;
        }
        public void IsValid()
        {
            if (!query.grant_type.Equals("password", StringComparison.OrdinalIgnoreCase))
            {
                throw new BadRequestException(_resourceManager.GetName(SharedResource.MustUseOAuthFlow));
            }

            if (selectedUser == null)
            {
                throw new BadRequestException(_resourceManager.GetName(SharedResource.UserOrPasswordNotCorrect));
            }

            if (!_userQueryRepository.PasswordIsCorrect(selectedUser, query.password))
            {
                throw new BadRequestException(_resourceManager.GetName(SharedResource.UserOrPasswordNotCorrect));
            }

        }
    }
}
