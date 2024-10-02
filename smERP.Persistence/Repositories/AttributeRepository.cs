using Microsoft.EntityFrameworkCore;
using smERP.Domain.Entities;
using smERP.Domain.Entities.Product;
using smERP.Application.Contracts.Persistence;
using smERP.Persistence.Data;
using System.Linq;
using Attribute = smERP.Domain.Entities.Product.Attribute;

namespace smERP.Persistence.Repositories;

public class AttributeRepository(ProductDbContext context) : Repository<Attribute>(context), IAttributeRepository
{
    public async Task<bool> DoesListExist(List<(int AttributeId, int AttributeValueId)> attributeValues)
    {
        if (attributeValues.Count == 0) return true;

        var existingCount = await _context.Set<AttributeValue>()
            .CountAsync(av => attributeValues.Select(x => x.AttributeId).Contains(av.AttributeId) && attributeValues.Select(x => x.AttributeValueId).Contains(av.Id));

        return existingCount == attributeValues.Count;
    }

    public override async Task<Attribute> GetByID(int id)
    {
        return await context.Attributes.Include(x => x.AttributeValues).FirstOrDefaultAsync(a => a.Id == id);
    }
}
