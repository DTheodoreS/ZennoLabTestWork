namespace backend.DatasetValidation
{
    using backend.Model;

    public class SelectRequiredOptionDatasetValidator : IDatasetValidator
    {
        public DatasetValidateResult Validate(Dataset dataset, DatasetArchive archive)
        {
            if (!dataset.ContainsCyrillic
                && !dataset.ContainsLatin
                && !dataset.ContainsNumbers)
            {
                return DatasetValidateResult.Invalid("Не выбрано ни одного обязательного параметра");
            }

            return DatasetValidateResult.Success;
        }
    }
}
