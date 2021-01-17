namespace backend.DatasetValidation
{
    using System;

    using backend.Model;

    public class NameMaxLengthDatasetValidator : IDatasetValidator
    {
        public DatasetValidateResult Validate(Dataset dataset, DatasetArchive archive)
        {
            if (String.IsNullOrEmpty(dataset.Name)
                || dataset.Name.Length < 4
                || dataset.Name.Length > 8)
            {
                return DatasetValidateResult.Invalid("Максимальная длина имени - от 4 до 8 символов");
            }

            return DatasetValidateResult.Success;
        }
    }
}
