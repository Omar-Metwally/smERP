using MediatR;
using smERP.Application.Features.Categories.Queries.Models;
using smERP.Application.Features.Categories.Queries.Responses;
using smERP.Domain.Entities.Product;
using smERP.Persistence.Contracts;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using System.Net;

namespace smERP.Application.Features.Categories.Queries.Handlers;

public class CategoriesQueryHandler(ICategoryRepository categoryRepository) : 
    IRequestHandler<GetCategoryByIdQuery, IResult<GetCategoryByIdResponse>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<IResult<GetCategoryByIdResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByID(request.CategoryId);

        if(category == null)
            return new Result<GetCategoryByIdResponse>().WithNotFound();

        var categoryResponse = new GetCategoryByIdResponse(category.Id, category.Name.English, category.Name.Arabic, category.ProductCount);
        return new Result<GetCategoryByIdResponse>(categoryResponse);
    }
}
