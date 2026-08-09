namespace HeatResponseToken.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; }

        public string Message { get; }

        private ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }

        public static ValidationResult Success()
        {
            return new ValidationResult(true, "検証成功");
        }

        public static ValidationResult Failure(string message)
        {
            return new ValidationResult(false, message);
        }
    }
}

