namespace backend.DatasetValidation
{
    using System;

    using backend.Model;

    public class ArchiveImagesCountEqualsAnswersCountDatasetValidator : IDatasetValidator
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
                && archive.ExistAnswers
                && archive.Files.Count - 1 != archive.AnswerEntryList.Count)
            {
                return DatasetValidateResult.Invalid("Количество ответов не совпадает с количеством картинок");
            }

            return DatasetValidateResult.Success;
        }
    }
}
