namespace smERP.Domain.Events;

public record ProductsQuantityChangedEvent(
    int TransactionId,
    string TransactionType,
    List<(int ProductIsntanceId, int EffectedQuantity, bool IsTracked, List<(string SerialNumber, string Status)>? SerialNumbers)> ProductEntries) : IDomainEvent;