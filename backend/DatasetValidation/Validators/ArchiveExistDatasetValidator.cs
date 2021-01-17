namespace backend.DatasetValidation
{
    using backend.Model;

    public class ArchiveExistDatasetValidator : IDatasetValidator
    {
        public DatasetValidateResult Validate(Dataset dataset, DatasetArchive archive)
        {
            if (archive == null || archive.WrongArchive)
            {
                return DatasetValidateResult.Invalid("Не задан архив картинок или не удалось его прочитать");
            }

            return DatasetValidateResult.Success;
        }
    }
}
