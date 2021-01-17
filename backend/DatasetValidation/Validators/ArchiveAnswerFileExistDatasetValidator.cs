namespace backend.DatasetValidation
{
    using System;

    using backend.Model;

    public class ArchiveAnswerFileExistDatasetValidator : IDatasetValidator
    {
        public DatasetValidateResult Validate(Dataset dataset, DatasetArchive archive)
        {
            if (archive == null || archive.WrongArchive)
            {
                // Возвращаем успешный ответ, так как есть правило на данную проверку
                // чтобы не дублировать ошибки пользователю
                return DatasetValidateResult.Success;
            }

            if (!String.IsNullOrEmpty(dataset.LocationOfResponsesToPictures)
                && dataset.LocationOfResponsesToPictures.Equals("в отдельном файле", StringComparison.OrdinalIgnoreCase)
                && !archive.ExistAnswers)
            {
                return DatasetValidateResult.Invalid("Файл ответов требуется, но отсутствует");
            }

            return DatasetValidateResult.Success;
        }
    }
}
