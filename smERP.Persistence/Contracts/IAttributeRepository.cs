using Attribute = smERP.Domain.Entities.Product.Attribute;

namespace smERP.Persistence.Contracts;

public interface IAttributeRepository : IRepository<Attribute>
{
    Task<bool> DoesListExist(List<(int AttributeId, int AttributeValueId)> AttributeValuesIds);
}
