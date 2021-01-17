namespace backend.Services
{
    using System.Collections.Generic;
    using System.IO;

    using backend.DatasetValidation;
    using backend.Model;

    public interface IDatasetService
    {
        List<Dataset> GetDatasetList();

        List<DatasetValidateResult> UploadDataset(Dataset dataset, MemoryStream archiveStream);
    }
}
