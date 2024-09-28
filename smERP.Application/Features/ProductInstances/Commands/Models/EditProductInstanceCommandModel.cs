using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.ProductInstances.Commands.Models;

public record EditProductInstanceCommandModel(
    int ProductId,
    int ProductInstanceId,
    int? QuantityInStock,
    decimal? SellingPrice,
    decimal? BuyingPrice,
    List<ProductInstanceAttributeValue> ProductInstanceAttributeValues) : IRequest<IResultBase>;