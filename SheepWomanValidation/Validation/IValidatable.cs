namespace SheepWomanValidation.Validation;

public interface IValidatable<T>
    where T : class
{
    ValidationResult Validate();
}
