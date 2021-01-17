namespace backend.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using backend.DatasetValidation;
    using backend.Model;
    using backend.Services;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class DatasetController : ControllerBase
    {
        private IDatasetService _datasetService;

        public DatasetController(IDatasetService datasetService)
        {
            _datasetService = datasetService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Dataset>>> Get()
        {
            var list = _datasetService.GetDatasetList();
            return await BuildResult(list);
        }

        [HttpPost]
        [RequestSizeLimit(100000000)]
        public async Task<ActionResult<List<DatasetValidateResult>>> Post([FromForm] Dataset dataset)
        {
            await Task.Delay(2000);
            var result = new List<DatasetValidateResult>();

            var file = Request.Form.Files.FirstOrDefault();
            using (var memoryStream = new MemoryStream())
            {
                if (file == null)
                {
                    result = _datasetService.UploadDataset(dataset, null);
                }
                else
                {
                    await file.CopyToAsync(memoryStream);
                    dataset.ArchiveOriginalName = file.FileName;
                    result = _datasetService.UploadDataset(dataset, memoryStream);
                }
            }

            return await BuildResult(result);
        }

        private Task<ActionResult<T>> BuildResult<T>(T result)
        {
            return Task.FromResult(new ActionResult<T>(result));
        }
    }
}
