namespace smERP.Application.Features.Categories.Queries.Responses;

public record GetCategoriesPaginatedListResponse(
    int CategoryID,
    string CategoryEnglishName,
    string CategoryArabicName,
    int ProductUnderCategoryCount);

