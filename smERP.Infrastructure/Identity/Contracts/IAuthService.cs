using smERP.Application.Features.Auth.Commands.Models;
using smERP.Application.Features.Auth.Commands.Results;
using smERP.SharedKernel.Responses;

namespace smERP.Infrastructure.Identity.Contracts
{
    public interface IAuthService
    {
        Task<IResult<LoginResult>> Login(LoginCommandModel request);
        Task<IResult<RegisterResult>> Register(RegisterCommandModel request);
        //Task<BaseResponse<string>> SendResetPasswordCode(string email);
        //Task<BaseResponse<string>> ConfirmAndResetPassword(string code, string email, string newPassword);

        Task<IResult<string>> CreateRole(CreateRoleCommandModel request);
        Task<IResult<string>> EditRole(EditRoleCommandModel request);
        Task<IResult<string>> DeleteRole(DeleteRoleCommandModel request);
        Task<IResult<string>> AddClaimToRole(AddClaimToRoleCommandModel request);
        Task<IResult<string>> AddRoleToUser(AssignRoleToUserCommandModel request);
        Task<IResult<string>> AddClaimToUser(AssignClaimToUserCommandModel request);
        Task<IResult<IEnumerable<string>>> GetUserRoles(string userId);
        Task<IResult<IEnumerable<string>>> GetUserClaims(string userId);
        Task<IResult<IEnumerable<string>>> GetRoleClaims(string roleName);
        Task<IResult<IEnumerable<string>>> GetAllRoles();
        Task<IResult<IEnumerable<string>>> GetAllClaims();
    }
}
