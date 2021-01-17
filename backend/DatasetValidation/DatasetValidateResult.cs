namespace backend.DatasetValidation
{
    public class DatasetValidateResult
    {
        public bool IsValid { get; set; }

        public string Message { get; set; }

        public static DatasetValidateResult Success
        {
            get
            {
                var result = new DatasetValidateResult();
                result.IsValid = true;
                return result;
            }
        }

        public static DatasetValidateResult Invalid(string message)
        {
            var result = new DatasetValidateResult();
            result.IsValid = false;
            result.Message = message;
            return result;
        }
    }
}
