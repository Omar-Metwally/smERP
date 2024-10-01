using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using smERP.Application.Features.Auth.Commands.Models;
using smERP.Application.Features.Auth.Commands.Results;
using smERP.Infrastructure.Identity.Contracts;
using smERP.Infrastructure.Identity.Models;
using smERP.Infrastructure.Identity.Models.Users;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace smERP.Infrastructure.Identity.Services
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        IdentityContext identityContext) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly IdentityContext _identityContext = identityContext;


        public async Task<IResult<LoginResult>> Login(LoginCommandModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                if (user != null) await _userManager.AccessFailedAsync(user);

                return new Result<LoginResult>()
                    .WithError(SharedResourcesKeys.PasswordOrEmailNotCorrect.Localize())
                    .WithStatusCode(HttpStatusCode.BadRequest);
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            if (!user.IsExistingRefreshTokenValid())
            {
                user.GenerateRefreshToken();
            }

            var loginResult = new LoginResult(jwtSecurityToken.ToString(), user.RefreshToken, user.RefreshTokenExpiration);

            return new Result<LoginResult>(loginResult);
        }

        public async Task<IResult<RegisterResult>> Register(RegisterCommandModel request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.FirstName + request.LastName);

            if (existingUser != null)
                return new Result<RegisterResult>()
                    .WithError(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.UserName))
                    .WithStatusCode(HttpStatusCode.BadRequest);

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
                return new Result<RegisterResult>()
                    .WithError(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.Email))
                    .WithStatusCode(HttpStatusCode.BadRequest);

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.FirstName + request.LastName,
                EmailConfirmed = true
            };


            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new Result<RegisterResult>()
                    .WithErrors(result.Errors.Select(a => a.Description).ToList())
                    .WithStatusCode(HttpStatusCode.BadRequest);

            await _userManager.AddToRoleAsync(user, "Employee");
            return new Result<RegisterResult>(new RegisterResult() { UserId = user.Id });

        }

        public Task<IResult<string>> AddClaimToRole(AddClaimToRoleCommandModel request)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<string>> AddClaimToUser(AssignClaimToUserCommandModel request)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<string>> AddRoleToUser(AssignRoleToUserCommandModel request)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<string>> CreateRole(CreateRoleCommandModel request)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<string>> DeleteRole(DeleteRoleCommandModel request)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<string>> EditRole(EditRoleCommandModel request)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<IEnumerable<string>>> GetAllClaims()
        {
            throw new NotImplementedException();
        }

        public Task<IResult<IEnumerable<string>>> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public Task<IResult<IEnumerable<string>>> GetRoleClaims(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<IEnumerable<string>>> GetUserClaims(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<IEnumerable<string>>> GetUserRoles(string userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<BaseResponse<string>> SendResetPasswordCode(string email)
        //{
        //    var trans = await _identityDbContext.Database.BeginTransactionAsync();

        //    try
        //    {
        //        var user = await _userManager.FindByEmailAsync(email);

        //        if (user == null)
        //            return NotFound<string>(_localizer["UserNotFound"]);

        //        var chars = "0123456789";
        //        var random = new Random();
        //        var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

        //        user.Code = randomNumber;

        //        var updateResult = await _userManager.UpdateAsync(user);

        //        if (!updateResult.Succeeded)
        //            return BadRequest<string>(_localizer["ErrorInUpdateUser"]);

        //        var message = $"Code To Reset Password: {user.Code}";

        //        await _emailsService.SendEmail(user.Email, message, "Reset Password");

        //        await trans.CommitAsync();

        //        return Success<string>(_localizer["Success"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        await trans.RollbackAsync();
        //        return BadRequest<string>(_localizer["Failed"]);
        //    }
        //}

        //public async Task<BaseResponse<string>> ConfirmAndResetPassword(string code, string email, string newPassword)
        //{
        //    using (var trans = await _identityDbContext.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            var user = await _userManager.FindByEmailAsync(email);

        //            if (user == null)
        //                return NotFound<string>(_localizer["UserNotFound"]);

        //            var userCode = user.Code;

        //            if (userCode == code)
        //            {
        //                // Code is valid, proceed to reset the password
        //                await _userManager.RemovePasswordAsync(user);
        //                await _userManager.AddPasswordAsync(user, newPassword);

        //                await trans.CommitAsync();

        //                return Success<string>(_localizer["PasswordResetSuccess"]);
        //            }

        //            return BadRequest<string>(_localizer["InvalidCode"]);
        //        }
        //        catch (Exception ex)
        //        {
        //            await trans.RollbackAsync();
        //            return BadRequest<string>(_localizer["Failed"]);
        //        }
        //    }
        //}

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

    }
}
