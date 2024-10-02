﻿using smERP.Application.Features.Auth.Commands.Models;
using smERP.Application.Features.Auth.Commands.Results;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Contracts.Infrastructure.Identity
{
    public interface IAuthService
    {
        Task<IResult<LoginResult>> Login(LoginCommandModel<IResult<LoginResult>> request);
        Task<IResult<RegisterResult>> Register(RegisterCommandModel<IResult<RegisterResult>> request);
        //Task<BaseResponse<string>> SendResetPasswordCode(string email);
        //Task<BaseResponse<string>> ConfirmAndResetPassword(string code, string email, string newPassword);

        Task<IResultBase> CreateRole(CreateRoleCommandModel request);
        Task<IResultBase> EditRole(EditRoleCommandModel request);
        Task<IResultBase> DeleteRole(DeleteRoleCommandModel request);
        Task<IResultBase> AddClaimToRole(AddClaimToRoleCommandModel request);
        Task<IResultBase> AddRoleToUser(AssignRoleToUserCommandModel request);
        Task<IResultBase> AddClaimToUser(AssignClaimToUserCommandModel request);
        Task<IResult<IEnumerable<string>>> GetUserRoles(string userId);
        Task<IResult<IEnumerable<(string Type, string Value)>>> GetUserClaims(string userId);
        Task<IResult<IEnumerable<(string Type, string Value)>>> GetRoleClaims(string roleName);
        Task<IResult<IEnumerable<string?>>> GetAllRoles();
    }
}