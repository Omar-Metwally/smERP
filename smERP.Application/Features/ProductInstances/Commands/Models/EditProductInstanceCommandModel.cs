using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.ProductInstances.Commands.Models;

public record EditProductInstanceCommandModel(
    int ProductId,
    int ProductInstanceId,
    decimal? SellingPrice,
    List<ProductInstanceAttributeValue> ProductInstanceAttributeValues) : IRequest<IResultBase>;