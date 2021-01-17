namespace backend.DatasetValidation
{
    using System;
    using System.Text.RegularExpressions;

    using backend.Model;

    public class NameCannotContainWordCaptchaDatasetValidator : IDatasetValidator
    {
        private static readonly string pattern = @"captcha";

        public DatasetValidateResult Validate(Dataset dataset, DatasetArchive archive)
        {
            if (String.IsNullOrEmpty(dataset.Name))
            {
                return DatasetValidateResult.Success;
            }

            if (Regex.IsMatch(dataset.Name, pattern, RegexOptions.IgnoreCase))
            {
                return DatasetValidateResult.Invalid("Имя не может содержать слово \"captcha\"");
            }

            return DatasetValidateResult.Success;
        }
    }
}
