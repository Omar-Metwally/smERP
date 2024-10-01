using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.StorageLocations.Comands.Models;

public record AddStorageLocationCommandModel(int BranchId, string Name) : IRequest<IResultBase>;
