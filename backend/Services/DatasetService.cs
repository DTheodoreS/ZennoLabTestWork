namespace backend.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using backend.DatasetValidation;
    using backend.Model;
    using backend.Repository;

    public class DatasetService : IDatasetService
    {
        private readonly IDatasetRepository _repository;

        private readonly IArchiveRepository _archiveRepository;

        private readonly IDatasetValidateProcessorFactory _datasetValidateProcessorFactory;

        public DatasetService(IDatasetRepository repository,
            IArchiveRepository archiveRepository,
            IDatasetValidateProcessorFactory datasetValidateProcessorFactory)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _archiveRepository = archiveRepository
                ?? throw new ArgumentNullException(nameof(archiveRepository));
            _datasetValidateProcessorFactory = datasetValidateProcessorFactory
                ?? throw new ArgumentNullException(nameof(datasetValidateProcessorFactory));
        }

        public List<Dataset> GetDatasetList()
        {
            return _repository.Get().ToList();
        }

        public List<DatasetValidateResult> UploadDataset(Dataset dataset, MemoryStream archiveStream)
        {
            var datasetArchive = archiveStream != null
                ? new DatasetArchive(archiveStream)
                : null;

            var datasetValidateProcessor = _datasetValidateProcessorFactory.Create(dataset, datasetArchive);
            if (!datasetValidateProcessor.IsValid)
            {
                var errors = datasetValidateProcessor.GetErrors();
                return errors;
            }

            var filename = _archiveRepository.GenerateUniqueName();
            dataset.ArchiveRepositoryName = filename;
            _repository.Create(dataset);

            _archiveRepository.Save(filename, archiveStream);

            return new List<DatasetValidateResult>();
        }
    }
}
