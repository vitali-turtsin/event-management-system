using System.ComponentModel.DataAnnotations;

namespace Extensions.Base.ValidationAttributes;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return true;
        return value is DateTime dateTime && dateTime > DateTime.UtcNow;
    }
}