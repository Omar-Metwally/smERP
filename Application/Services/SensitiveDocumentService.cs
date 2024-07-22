using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using smERP.Application.DTOs.Results;
using smERP.Application.Interfaces;
using smERP.Application.Interfaces.Services;
using smERP.Domain.Entities.Transactions;
using System.Security.Cryptography;

namespace smERP.Application.Services;

public class SensitiveDocumentService : BaseResponseHandler, ISensitiveDocumentService
{
    private readonly IUnitOfWork<SensitiveDocument> _unitOfWork;
    private readonly IStringLocalizer<SensitiveDocumentService> _localizer;
    private readonly IConfiguration _configuration;

    public SensitiveDocumentService(
        IUnitOfWork<SensitiveDocument> unitOfWork,
        IStringLocalizer<SensitiveDocumentService> localizer,
        IStringLocalizer<BaseResponseHandler> baseLocalizer,
        IConfiguration configuration)
        : base(baseLocalizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _configuration = configuration;
    }

    public async Task<BaseResponse<int>> UploadDocument(IFormFile file, string userName)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileBytes = memoryStream.ToArray();

        var encryptedBytes = EncryptData(fileBytes);

        var document = new SensitiveDocument
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            EncryptedContent = encryptedBytes,
            UploadedById = userName,
            UploadDate = DateTime.UtcNow
        };

        var uploadedDocument = await _unitOfWork.Repository.Add(document);
        await _unitOfWork.SaveChangesAsync();

        return Success(uploadedDocument.Id, _localizer["DocumentUploaded"]);
    }

    public async Task<BaseResponse<FileContentResult>> GetDocument(int id, string userName)
    {
        var document = await _unitOfWork.Repository.GetByID(id);

        if (document == null)
            return NotFound<FileContentResult>(_localizer["DocumentNotFound"]);

        // Here you might want to add additional authorization checks

        var decryptedBytes = DecryptData(document.EncryptedContent);

        var fileContentResult = new FileContentResult(decryptedBytes, document.ContentType)
        {
            FileDownloadName = document.FileName
        };

        return Success(fileContentResult, _localizer["DocumentRetrieved"]);
    }

    private byte[] EncryptData(byte[] data)
    {
        using var aes = Aes.Create();
        aes.Key = Convert.FromBase64String(_configuration["Encryption:Key"]);
        aes.IV = Convert.FromBase64String(_configuration["Encryption:IV"]);

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        return PerformCryptography(data, encryptor);
    }

    private byte[] DecryptData(byte[] data)
    {
        using var aes = Aes.Create();
        aes.Key = Convert.FromBase64String(_configuration["Encryption:Key"]);
        aes.IV = Convert.FromBase64String(_configuration["Encryption:IV"]);

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        return PerformCryptography(data, decryptor);
    }

    private byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
    {
        using var ms = new MemoryStream();
        using var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);
        cryptoStream.Write(data, 0, data.Length);
        cryptoStream.FlushFinalBlock();
        return ms.ToArray();
    }
}

