namespace backend.DatasetValidation
{
    using System;
    using System.Text.RegularExpressions;

    using backend.Model;

    public class NameContainsOnlyLatinLettersDatasetValidator : IDatasetValidator
    {
        private static readonly string _pattern = @"[abcdefghigklmnopqrstuvwxyz]";

        public DatasetValidateResult Validate(Dataset dataset, DatasetArchive archive)
        {
            if (String.IsNullOrEmpty(dataset.Name)) {
                return DatasetValidateResult.Success;
            }

            char[] textArray = dataset.Name.ToCharArray();
            for (int i = 0; i < textArray.Length; i++)
            {
                if (!Regex.IsMatch(textArray[i].ToString(), _pattern, RegexOptions.IgnoreCase))
                {
                    return DatasetValidateResult.Invalid("Имя должно содержать только латинские буквы");
                }
            }

            return DatasetValidateResult.Success;
        }
    }
}
