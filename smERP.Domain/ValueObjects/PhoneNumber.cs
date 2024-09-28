
namespace smERP.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; }
    public string Number { get; }
    public string Comment { get; }

    public PhoneNumber(string countryCode, string number, string comment)
    {
        CountryCode = countryCode;
        Number = number;
        Comment = comment;
    }

    //public static Result<PhoneNumber> Create(string phoneNumber)
    //{
    //    // Remove any non-digit characters
    //    var digitsOnly = Regex.Replace(phoneNumber, @"\D", "");

    //    if (string.IsNullOrWhiteSpace(digitsOnly))
    //        return Result<PhoneNumber>.Failure("Phone number cannot be empty.");

    //    if (digitsOnly.Length < 10 || digitsOnly.Length > 15)
    //        return Result<PhoneNumber>.Failure("Phone number must be between 10 and 15 digits.");

    //    string countryCode = "";
    //    string number = digitsOnly;

    //    // If the number starts with a plus, assume the first 1-3 digits are the country code
    //    if (phoneNumber.StartsWith("+"))
    //    {
    //        int countryCodeLength = Math.Min(3, digitsOnly.Length - 9);
    //        countryCode = digitsOnly.Substring(0, countryCodeLength);
    //        number = digitsOnly.Substring(countryCodeLength);
    //    }

    //    return Result<PhoneNumber>.Success(new PhoneNumber(countryCode, number));
    //}

    public string ToFormattedString()
    {
        if (!string.IsNullOrEmpty(CountryCode))
            return $"+{CountryCode} {FormatNationalNumber()}";
        return FormatNationalNumber();
    }

    private string FormatNationalNumber()
    {
        // This is a simple formatting. You might want to adjust based on specific country formats.
        if (Number.Length == 10)
            return $"({Number.Substring(0, 3)}) {Number.Substring(3, 3)}-{Number.Substring(6)}";
        return Number;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
    }

    public override string ToString()
    {
        return ToFormattedString();
    }
}
