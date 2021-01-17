namespace backend.Repository
{
    using System.Collections.Generic;

    using backend.Model;

    public interface IDatasetRepository
    {
        IEnumerable<Dataset> Get();

        void Create(Dataset dataset);
    }
}
