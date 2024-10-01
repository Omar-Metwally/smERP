
using smERP.Domain.Entities.Product;
using smERP.Domain.ValueObjects;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using System.Net;

namespace smERP.Domain.Entities.Organization;

public class Branch : Entity, IAggregateRoot
{
    public BilingualName Name { get; private set; } = null!;
    public ICollection<StorageLocation> StorageLocations { get; private set; } = null!;
    //public string BranchManagerId { get; private set; }
    //public int CompanyId { get; private set; }
    //public virtual Employee BranchManger { get; private set; }
    //public ICollection<Employee> BranchEmployees { get; private set; }
    private Branch(BilingualName name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    private Branch() { }

    public static IResult<Branch> Create(string englishName, string arabicName)
    {
        var nameResult = BilingualName.Create(englishName, arabicName);
        if (nameResult.IsFailed)
            return nameResult.ChangeType(new Branch());

        return new Result<Branch>(new Branch(nameResult.Value));
    }

    public IResult<StorageLocation> AddStorageLocation(string name)
    {
        if (string.IsNullOrEmpty(name))
            return new Result<StorageLocation>()
                .WithError(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Name.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        var storageLocationToBeCreated = new StorageLocation(Id, name);

        if (StorageLocations == null)
        {
            StorageLocations = [storageLocationToBeCreated];
            return new Result<StorageLocation>(storageLocationToBeCreated);
        }

        if (StorageLocations.Any(av => av.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            return new Result<StorageLocation>()
                .WithError(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.Name.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        StorageLocations.Add(storageLocationToBeCreated);

        return new Result<StorageLocation>(storageLocationToBeCreated);
    }

    public IResult<StorageLocation> UpdateStorageLocation(int storageLocationId, string name)
    {
        if (string.IsNullOrEmpty(name))
            return new Result<StorageLocation>()
                .WithError(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Name.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        if (storageLocationId < 0)
            return new Result<StorageLocation>()
                .WithError(SharedResourcesKeys.___MustBeAPositiveNumber.Localize(SharedResourcesKeys.StorageLocation.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        var storageLocationToBeUpdated = StorageLocations.FirstOrDefault(x => x.Id == storageLocationId);
        if (storageLocationToBeUpdated == null)
            return new Result<StorageLocation>()
                .WithError(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.StorageLocation.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        if (StorageLocations.Any(av => av.Name.Equals(storageLocationToBeUpdated.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return new Result<StorageLocation>()
                .WithError(SharedResourcesKeys.DoesExist.Localize(SharedResourcesKeys.Name.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        return new Result<StorageLocation>(storageLocationToBeUpdated);
    }

    public IResult<StorageLocation> RemoveStorageLocation(int storageLocationId)
    {
        if (storageLocationId < 0)
            return new Result<StorageLocation>()
                .WithError(SharedResourcesKeys.___MustBeAPositiveNumber.Localize(SharedResourcesKeys.StorageLocation.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        var storageLocationToBeDeleted = StorageLocations.FirstOrDefault(x => x.Id == storageLocationId);
        if (storageLocationToBeDeleted == null)
            return new Result<StorageLocation>()
                .WithError(SharedResourcesKeys.DoesNotExist.Localize(SharedResourcesKeys.StorageLocation.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        StorageLocations.Remove(storageLocationToBeDeleted);

        return new Result<StorageLocation>(storageLocationToBeDeleted);
    }

    //public void SetBranchManager(string branchManagerId)
    //{
    //    if (string.IsNullOrWhiteSpace(branchManagerId))
    //        throw new ArgumentException("Branch manager ID cannot be empty.", nameof(branchManagerId));

    //    BranchManagerId = branchManagerId;
    //}

    //public void AddEmployee(Employee employee)
    //{
    //    if (employee == null)
    //        throw new ArgumentNullException(nameof(employee));

    //    BranchEmployees.Add(employee);
    //}
}
