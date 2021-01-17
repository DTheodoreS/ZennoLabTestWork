namespace backend.DatasetValidation
{
    using backend.Model;

    public class ArchiveImagesCountDatasetValidator : IDatasetValidator
    {
        public DatasetValidateResult Validate(Dataset dataset, DatasetArchive archive)
        {
            if (archive == null || archive.WrongArchive)
            {
                // Возвращаем успешный ответ, так как есть правило на данную проверку
                // чтобы не дублировать ошибки пользователю
                return DatasetValidateResult.Success;
            }

            var delta = 0;

            if (dataset.ContainsCyrillic)
                delta += 3000;

            if (dataset.ContainsLatin)
                delta += 3000;

            if (dataset.ContainsNumbers)
                delta += 3000;

            if (dataset.ContainsSpecialCharacters)
                delta += 3000;

            if (dataset.CaseSensitivity)
                delta += 3000;

            var minLimit = 2000 + delta;
            var maxLimit = 3000 + delta;

            if (archive.Files.Count < minLimit
                || archive.Files.Count > maxLimit)
            {
                return DatasetValidateResult.Invalid("Кол-во картинок в архиве не соответствует указанным параметрам");
            }            

            return DatasetValidateResult.Success;
        }
    }
}
