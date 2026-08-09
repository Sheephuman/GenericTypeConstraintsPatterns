
using HeatResponseToken.interfaces;

namespace HeatResponseToken.Models
{
    public class ValidationToken<T>
        where T : IValidatable
    {
        public T Value { get; }

        public ValidationResult ValidationResult { get; }



        private ValidationToken(
            T value,
            ValidationResult validationResult)
        {
            Value = value;
            ValidationResult = validationResult;
        }

      



        public static (ValidationResult Result, ValidationToken<T>? Token)
            Create(T value)
        {
            var result = value.Validate();

            if (!result.IsValid)
            {
                return (result, null);
            }

            return (
                result,
                new ValidationToken<T>(value, result));
        }


    }
}

