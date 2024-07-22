using Microsoft.AspNetCore.Mvc;
using smERP.Application.DTOs.Results;

namespace smERP.Application.Interfaces.Services;

public interface ISensitiveDocumentService
{
    Task<BaseResponse<int>> UploadDocument(IFormFile file, string userName);
    Task<BaseResponse<FileContentResult>> GetDocument(int id, string userName);
}
