namespace HR_Assist.Core.Services.Accounts
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using HR_Assist.Core.Entities;
    using HR_Assist.Core.Entities.Contexts;
    using HR_Assist.Core.Services.Common.Models;
    using HR_Assist.Core.Helpers;

    public class UserLoginHandler : IRequestHandler<UserLoginRequest, ResponseModel>
    {
        private readonly HR_AssistDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserLoginHandler(HR_AssistDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _db = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<ResponseModel> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                   ?? await _userManager.FindByNameAsync(request.Email);

            var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
            if (passwordIsCorrect)
            {

                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
               
                claims.AddRange(JwtHelper.GenerateClaims(ClaimTypes.Role, roles.ToList()));

                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                if (claims != null && claimsPrincipal?.Identity is ClaimsIdentity claimsIdentity)
                {
                    claimsIdentity.AddClaims(claims);
                    await _signInManager.SignInWithClaimsAsync(user,
                    true,
                    claimsIdentity.Claims);
                }

                return new ResponseModel
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new
                    {
                        access_token = JwtHelper.GenerateJwtToken(claims, _configuration),
                        role = roles.ToList()
                    }
                };
            }
            else
            {
                return new ResponseModel
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "User or Password is invalid"
                };
            }
        }
    }
}
