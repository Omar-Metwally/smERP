using Microsoft.AspNetCore.Mvc;
using smERP.API.Base;
using smERP.Application.DTOs.Rquests.Company;
using smERP.Application.Helpers;
using smERP.Application.Interfaces.Services;
using System.Globalization;

namespace smERP.API.Controllers;

[Route("[controller]")]
[ApiController]
public class CompanyController(ICompanyService companyService, ILogger<CompanyController> logger) : APIControllerBase
{
    private readonly ICompanyService _companyService = companyService;

    private readonly ILogger<APIControllerBase> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> CreateCompany(CreateCompanyRequest request)
    {
        return CustomResult(await _companyService.CreateCompany(request));
    }
}
