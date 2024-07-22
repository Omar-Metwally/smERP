namespace smERP.Domain.Entities.Transactions;

public class TransactionDocument
{
    public int SensitiveDocumentId { get; set; }
    public int TransactionId { get; set; }
    public virtual SensitiveDocument SensitiveDocument { get; set; }
    public virtual BaseTransaction Transaction { get; set; }
}
