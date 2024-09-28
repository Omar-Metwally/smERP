using smERP.Domain.ValueObjects;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using System.Net;

namespace smERP.Domain.Entities.Product;

public class Brand : Entity, IAggregateRoot
{
    public BilingualName Name { get; private set; } = null!;
    public virtual IReadOnlyCollection<Product> Products { get; } = new List<Product>();
    private bool IsHidden { get; set; }

    private Brand(BilingualName name)
    {
        Name = name;
        IsHidden = true;
    }

    private Brand() { }

    public static IResult<Brand> Create(string englishName, string arabicName)
    {
        var nameResult = BilingualName.Create(englishName, arabicName);
        if (nameResult.IsFailed)
            return nameResult.ChangeType(new Brand());

        return new Result<Brand>(new Brand(nameResult.Value))
            .WithStatusCode(HttpStatusCode.Created)
            .WithMessage(SharedResourcesKeys.Created.Localize());
    }

    public IResult<Brand> UpdateName(string englishName, string arabicName)
    { 
        var nameResult = BilingualName.Create(englishName, arabicName);
        if (nameResult.IsFailed)
            return nameResult.ChangeType(new Brand());

        Name = nameResult.Value;
        return new Result<Brand>(this)
            .WithStatusCode(HttpStatusCode.OK)
            .WithMessage(SharedResourcesKeys.UpdatedSuccess.Localize());
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public void Show()
    {
        IsHidden = false;
    }
}
