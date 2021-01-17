namespace backend.DatasetValidation
{
    using System.Collections.Generic;

    using backend.Model;

    public class DatasetValidateProcessorFactory : IDatasetValidateProcessorFactory
    {
        private readonly IEnumerable<IDatasetValidator> _validators;

        public DatasetValidateProcessorFactory(IEnumerable<IDatasetValidator> validators)
        {
            _validators = validators ?? throw new System.ArgumentNullException(nameof(validators));
        }

        public IDatasetValidateProcessor Create(Dataset dataset, DatasetArchive archive)
        {
            return new DatasetValidateProcessor(_validators, dataset, archive);
        }
    }
}
