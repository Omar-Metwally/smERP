using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Transactions;

public class SensitiveDocument : BaseEntity
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] EncryptedContent { get; set; }
    public string UploadedById { get; set; }
    public DateTime UploadDate { get; set; }
    public virtual Employee UploadedBy { get; set; }
}
