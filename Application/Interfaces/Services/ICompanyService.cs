using smERP.Application.DTOs.Results;
using smERP.Application.DTOs.Rquests.Company;

namespace smERP.Application.Interfaces.Services;

public interface ICompanyService
{
    Task<BaseResponse<int>> CreateCompany(CreateCompanyRequest request);
}
