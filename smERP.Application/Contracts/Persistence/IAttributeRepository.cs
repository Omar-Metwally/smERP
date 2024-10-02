using Attribute = smERP.Domain.Entities.Product.Attribute;

namespace smERP.Application.Contracts.Persistence;

public interface IAttributeRepository : IRepository<Attribute>
{
    Task<bool> DoesListExist(List<(int AttributeId, int AttributeValueId)> AttributeValuesIds);
}
