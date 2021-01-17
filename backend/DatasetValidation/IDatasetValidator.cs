namespace backend.DatasetValidation
{
    using backend.Model;

    public interface IDatasetValidator
    {
        DatasetValidateResult Validate(Dataset dataset, DatasetArchive archive);
    }
}
