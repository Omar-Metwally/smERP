﻿using smERP.Enums;

namespace smERP.Domain.Entities.Transactions;

public class TransactionChange
{
    public int TransactionChangeId { get; set; }
    public int TransactionId { get; set; }
    public DBAction ChangeType { get; set; }
    public DateTime ChangedAt { get; set; }
    public string ChangedField { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string? Comment { get; set; }
    public virtual BaseTransaction BaseTransaction { get; set; }
}
