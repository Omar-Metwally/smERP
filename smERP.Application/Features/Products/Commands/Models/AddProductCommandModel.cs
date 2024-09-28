﻿using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.Products.Commands.Models;

public record AddProductCommandModel(
    string EnglishName,
    string ArabicName,
    string ModelNumber,
    int BrandId,
    int CategoryId,
    string? Description) : IRequest<IResultBase>;