﻿using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.StorageLocations.Comands.Models;

public record DeleteStorageLocationCommandModel(int BranchId, int StorageLocationId) : IRequest<IResultBase>;