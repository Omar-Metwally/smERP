using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.ProductInstances.Commands.Models;

public record AddProductInstanceCommandModel(
    int ProductId,
    int? QuantityInStock,
    decimal? SellingPrice,
    decimal? BuyingPrice,
    List<ProductInstanceAttributeValue> ProductInstanceAttributeValues) : IRequest<IResultBase>;

public record ProductInstanceAttributeValue(
    int AttributeId,
    int AttributeValueId);