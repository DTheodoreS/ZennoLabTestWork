namespace backend.DatasetValidation
{
    using System.Collections.Generic;
    using System.Linq;

    using backend.Model;

    public class DatasetValidateProcessor : IDatasetValidateProcessor
    {
        private readonly IEnumerable<IDatasetValidator> _validators;

        private readonly Dataset _dataset;

        private readonly DatasetArchive _archive;

        private bool _validateComplete;

        private List<DatasetValidateResult> _validateResultList;

        public DatasetValidateProcessor(IEnumerable<IDatasetValidator> validators, Dataset dataset, DatasetArchive archive)
        {
            _validators = validators ?? throw new System.ArgumentNullException(nameof(validators));
            _dataset = dataset ?? throw new System.ArgumentNullException(nameof(dataset));
            _archive = archive;
        }

        public bool IsValid
        {
            get
            {
                Validate();
                return _validateResultList.All(v => v.IsValid);
            }
        }

        public List<DatasetValidateResult> GetErrors()
        {
            return _validateResultList
                .Where(v => !v.IsValid)
                .ToList();
        }

        private void Validate()
        {
            if (_validateComplete)
                return;

            _validateComplete = true;
            _validateResultList = _validators
                .Select(v => v.Validate(_dataset, _archive))
                .ToList();
        }
    }
}
