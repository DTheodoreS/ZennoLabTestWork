namespace backend.DatasetValidation
{
    using backend.Model;

    public interface IDatasetValidateProcessorFactory
    {
        IDatasetValidateProcessor Create(Dataset dataset, DatasetArchive archive);
    }
}
