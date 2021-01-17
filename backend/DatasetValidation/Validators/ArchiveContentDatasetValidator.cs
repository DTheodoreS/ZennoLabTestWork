namespace backend.DatasetValidation
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using backend.Model;

    public class ArchiveContentDatasetValidator : IDatasetValidator
    {
        private List<string> SupportedExtensions
        {
            get
            {
                return new List<string> {
                    ".jpg",
                    ".jpeg",
                    ".bmp",
                    ".png",
                    ".gif",
                };                    
            }
        }

        public DatasetValidateResult Validate(Dataset dataset, DatasetArchive archive)
        {
            if (archive == null || archive.WrongArchive)
            {
                // Возвращаем успешный ответ, так как есть правило на данную проверку
                // чтобы не дублировать ошибки пользователю
                return DatasetValidateResult.Success;
            }

            foreach (var file in archive.Files)
            {
                if (file.Equals("answers.txt", StringComparison.OrdinalIgnoreCase))
                    continue;

                var extension = Path.GetExtension(file);
                if (!IsSupportedExtension(extension))
                {
                    return DatasetValidateResult.Invalid("Архив должен содержать только картинки и/или файл с ответами");
                }
            }

            return DatasetValidateResult.Success;
        }

        private bool IsSupportedExtension(string extension)
        {
            foreach (var supportedExtension in SupportedExtensions)
            {
                if (!extension.Equals(supportedExtension, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
