namespace smERP.Application.Features.Categories.Queries.Responses;

public record GetCategoryByIdResponse(
    int CategoryID,
    string CategoryEnglishName,
    string CategoryArabicName,
    int ProductUnderCategoryCount);