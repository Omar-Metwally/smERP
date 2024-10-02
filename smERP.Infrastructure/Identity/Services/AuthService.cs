using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using smERP.Application.Contracts.Infrastructure.Identity;
using smERP.Application.Features.Auth.Commands.Models;
using smERP.Application.Features.Auth.Commands.Results;
using smERP.Infrastructure.Identity.Models;
using smERP.Infrastructure.Identity.Models.Users;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace smERP.Infrastructure.Identity.Services;

public class AuthService(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<JwtSettings> jwtSettings) : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<IResult<LoginResult>> Login(LoginCommandModel<IResult<LoginResult>> request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            if (user != null) await _userManager.AccessFailedAsync(user);

            return new Result<LoginResult>()
                .WithError(SharedResourcesKeys.PasswordOrEmailNotCorrect.Localize())
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        if (!user.IsExistingRefreshTokenValid())
        {
            user.GenerateRefreshToken();
        }

        var loginResult = new LoginResult(jwtSecurityToken.ToString(), user.RefreshToken, user.RefreshTokenExpiration);

        return new Result<LoginResult>(loginResult);
    }

    public async Task<IResult<RegisterResult>> Register(RegisterCommandModel<IResult<RegisterResult>> request)
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
            EmailConfirmed = true,
        };

        //user.Employee.BranchId = request.BranchId;
        user.AddBranch(request.BranchId);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return new Result<RegisterResult>()
                .WithErrors(result.Errors.Select(a => a.Description).ToList())
                .WithStatusCode(HttpStatusCode.BadRequest);

        //await _userManager.AddToRoleAsync(user, "Employee");
        return new Result<RegisterResult>(new RegisterResult() { UserId = user.Id });

    }
    public async Task<IResultBase> CreateRole(CreateRoleCommandModel request)
    {
        var doesRoleExist = await _roleManager.RoleExistsAsync(request.RoleName);
        if (doesRoleExist)
            return new Result<string>()
                .WithBadRequest(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.Role.Localize()));

        var roleToBeCreated = new IdentityRole(request.RoleName);
        var roleToBeCreatedResult = await _roleManager.CreateAsync(roleToBeCreated);

        if (!roleToBeCreatedResult.Succeeded)
            return new Result<string>()
                .WithBadRequest(roleToBeCreatedResult.Errors.Select(x => x.Description));

        return new Result<string>();
    }

    public async Task<IResultBase> EditRole(EditRoleCommandModel request)
    {
        var roleToBeEdited = await _roleManager.FindByNameAsync(request.RoleName);
        if (roleToBeEdited == null)
            return new Result<string>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Role.Localize()));

        roleToBeEdited.Name = request.NewRoleName;

        var roleToBeEditedResult = await _roleManager.UpdateAsync(roleToBeEdited);
        if (!roleToBeEditedResult.Succeeded)
            return new Result<string>()
                .WithBadRequest(roleToBeEditedResult.Errors.Select(x => x.Description));

        return new Result<string>();
    }

    public async Task<IResultBase> DeleteRole(DeleteRoleCommandModel request)
    {
        var roleToBeDeleted = await _roleManager.FindByNameAsync(request.RoleName);
        if (roleToBeDeleted == null)
            return new Result<string>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Role.Localize()));

        var roleToBeDeletedResult = await _roleManager.DeleteAsync(roleToBeDeleted);
        if (!roleToBeDeletedResult.Succeeded)
            return new Result<string>()
                .WithBadRequest(roleToBeDeletedResult.Errors.Select(x => x.Description));

        return new Result<string>();
    }

    public async Task<IResultBase> AddClaimToRole(AddClaimToRoleCommandModel request)
    {
        var role = await _roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
            return new Result<string>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Role.Localize()));

        var roleClaims = await _roleManager.GetClaimsAsync(role);

        var doesClaimRoleExist = roleClaims.Any(x => x.ValueType == request.ClaimType && x.Value == request.ClaimValue);
        if (doesClaimRoleExist)
        return new Result<string>()
            .WithBadRequest(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.Claim.Localize()));

        var claimRoleToBeCreated = new Claim(request.ClaimType, request.ClaimValue);

        var claimRoleToBeCreatedResult = await _roleManager.AddClaimAsync(role, claimRoleToBeCreated);
        if (!claimRoleToBeCreatedResult.Succeeded)
            return new Result<string>()
                .WithBadRequest(claimRoleToBeCreatedResult.Errors.Select(x => x.Description));

        return new Result<string>();
    }

    public async Task<IResultBase> AddClaimToUser(AssignClaimToUserCommandModel request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return new Result<string>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.User.Localize()));

        var existingUserClaims = await _userManager.GetClaimsAsync(user);

        var doesUserClaimExist = existingUserClaims.Any(x => x.ValueType == request.ClaimType && x.Value == request.ClaimValue);
        if (doesUserClaimExist)
            return new Result<string>()
                .WithBadRequest(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.User.Localize()));

        var userClaimToBeCreated = new Claim(request.ClaimType, request.ClaimValue);

        var userClaimToBeCreatedResult = await _userManager.AddClaimAsync(user, userClaimToBeCreated);
        if (!userClaimToBeCreatedResult.Succeeded)
          return new Result<string>()
                .WithBadRequest(userClaimToBeCreatedResult.Errors.Select(x => x.Description));

        return new Result<string>();
    }

    public async Task<IResultBase> AddRoleToUser(AssignRoleToUserCommandModel request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return new Result<string>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.User.Localize()));

        var role = await _roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
            return new Result<string>()
                .WithBadRequest(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Role.Localize()));

        var userRoles = await _userManager.GetRolesAsync(user);

        var doesUserRoleExist = userRoles.Any(x => x == request.RoleName);
        if (doesUserRoleExist)
            return new Result<string>()
                .WithBadRequest(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.Role.Localize()));

        var userRoleCreateResult = await _userManager.AddToRoleAsync(user, request.RoleName);
        if (!userRoleCreateResult.Succeeded)
            return new Result<string>()
                .WithBadRequest(userRoleCreateResult.Errors.Select(x => x.Description));

        return new Result<string>();
    }

    public async Task<IResult<IEnumerable<string?>>> GetAllRoles()
    {
        var roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
        return new Result<IEnumerable<string?>>(roles ?? new List<string?>());
    }

    public async Task<IResult<IEnumerable<(string Type, string Value)>>> GetRoleClaims(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
            return new Result<IEnumerable<(string, string)>>()
                .WithBadRequestResult(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.Role.Localize()));

        var roleClaims = await _roleManager.GetClaimsAsync(role);

        return new Result<IEnumerable<(string, string)>>(roleClaims.Select(x => (x.Type, x.Value)));
    }

    public async Task<IResult<IEnumerable<(string Type, string Value)>>> GetUserClaims(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new Result<IEnumerable<(string, string)>>()
                .WithBadRequestResult(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.User.Localize()));

        var userClaims = await _userManager.GetClaimsAsync(user);

        return new Result<IEnumerable<(string, string)>>(userClaims.Select(x => (x.Type, x.Value)));
    }

    public async Task<IResult<IEnumerable<string>>> GetUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new Result<IEnumerable<string>>()
                .WithBadRequestResult(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.User.Localize()));

        var userRoles = await _userManager.GetRolesAsync(user);

        return new Result<IEnumerable<string>>(userRoles);
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
