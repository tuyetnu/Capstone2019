using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.Debug.ChangeUserRole;
using DormyWebService.ViewModels.UserModelViews;
using DormyWebService.ViewModels.UserModelViews.CheckToken;
using DormyWebService.ViewModels.UserModelViews.GetAllStudent;
using DormyWebService.ViewModels.UserModelViews.GetUser;
using DormyWebService.ViewModels.UserModelViews.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IMapper _mapper;

        private readonly ISieveProcessor _sieveProcessor;

        // used to get secret from appsettings
        private readonly AuthenticationSetting _authSettings;
        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=";

        public UserService(IOptions<AuthenticationSetting> authSettings, IRepositoryWrapper repoWrapper,
            ISieveProcessor sieveProcessor, IMapper mapper)
        {
            _authSettings = authSettings.Value;
            _repoWrapper = repoWrapper;
            _sieveProcessor = sieveProcessor;
            _mapper = mapper;
        }

        public async Task<List<GetUserResponse>> AdvancedGetUser(string sorts, string filters, int? page, int? pageSize)
        {
            var sieveModel = new SieveModel()
            {
                Sorts = sorts,
                Page = page,
                PageSize = pageSize,
                Filters = filters,
            };

            var users = await _repoWrapper.User.FindAllAsync();

            if (users == null || users.Any() == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "UserService: No user is found");
            }

            var sortedUsers = _sieveProcessor.Apply(sieveModel, users.AsQueryable()).ToList();

            return sortedUsers.Select(user => _mapper.Map<GetUserResponse>(user)).ToList();
        }

        public async Task<List<User>> DebugFindAll()
        {
            return (List<User>) await _repoWrapper.User.FindAllAsync();
        }

        public async Task<User> FindById(int id)
        {
            var user = await _repoWrapper.User.FindByIdAsync(id);

            //Check if there's this user in database
            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "User is not found");
            }

            return user;
        }

        public async Task<LoginSuccessUser> Authenticate(string idToken, string email)
        {
            //TODO:Nhớ bắt phải tài khoản FPT ko

            //Call Google API with Id Token
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(GoogleApiTokenInfoUrl + idToken))
                {
                    var test = await response.Content.ReadAsStringAsync();
                    try
                    {
                        //Covert Json to IdToken
                        var idTokenResponse = JsonConvert.DeserializeObject<IdTokenResponse>(test);

                        //Log for debug
                        System.Diagnostics.Debug.WriteLine("idToken.Email: " + idTokenResponse.Email);
                        System.Diagnostics.Debug.WriteLine("Sent Email: " + email);

                        //Check if email sent and email form Google API are the same
                        if (idTokenResponse.Email != email || string.IsNullOrEmpty(idTokenResponse.Email))
                        {
                            throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                                "UserService: Email is not the same from Google API");
                        }
                    }

                    catch (JsonSerializationException)
                    {
                        throw new HttpStatusCodeException(HttpStatusCode.BadRequest,
                            "UserService: Could not deserialize message form Google API");
                    }
                }
            }

            //Find User with the same email in database
            var user = await _repoWrapper.User.FindAsync(u => u.Email == email);

            //Check if user is null
            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "This account doesn't exist'");
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return new LoginSuccessUser()
            {
                Role = user.Role,
                AccessToken = tokenHandler.WriteToken(token),
                Id = user.UserId,
                Status = user.Status,
                Email = user.Email
            };
        }

        public async Task<User> ChangeStatus(int id, string status)
        {
            var user = await _repoWrapper.User.FindByIdAsync(id);

            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound);
            }

            user.Status = status;
            user = await _repoWrapper.User.UpdateAsync(user, id);

            return user;
        }

        public async Task<DebugChangeUserRoleResponse> ChangeUserRole(int id, string role)
        {
            var user = await _repoWrapper.User.FindByIdAsync(id);

            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "User not found");
                ;
            }

            user.Role = role;

            user = await _repoWrapper.User.UpdateAsync(user, id);

            return new DebugChangeUserRoleResponse()
            {
                Role = user.Role,
                UserId = user.UserId
            };
        }
        
        public async Task<ActionResult<CheckTokenResponse>> CheckTokenAsync(int userId)
        {
            var user = await _repoWrapper.User.FindByIdAsync(userId);
            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "User not found");
            }
            return new CheckTokenResponse(user);
        }
    }
}