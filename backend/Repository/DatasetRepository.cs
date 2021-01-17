namespace backend.Repository
{
    using System.Collections.Generic;

    using backend.Model;

    public class DatasetRepository : IDatasetRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DatasetRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Dataset dataset)
        {
            _dbContext.DatasetList.Add(dataset);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Dataset> Get()
        {
            return _dbContext.DatasetList;
        }
    }
}
