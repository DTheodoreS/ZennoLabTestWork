namespace backend.tests
{
    using backend.DatasetValidation;
    using backend.Model;

    using Microsoft.Extensions.DependencyInjection;

    using Xunit;

    public class DatasetValidateTests : IClassFixture<TestFixture>
    {
        private ServiceProvider _serviceProvider;

        public DatasetValidateTests(TestFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void SuccessValidationDataset()
        {
            var datasetValidateProcessorFactory = _serviceProvider.GetService<IDatasetValidateProcessorFactory>();

            var dataset = new Dataset();
            dataset.Name = "testing";
            dataset.ContainsLatin = true;

            var datasetArchive = new DatasetArchive();
            for (int i = 0; i < 5500; i++)
            {
                datasetArchive.Files.Add($"image{i}.jpg");
            }

            var datasetValidateProcessor = datasetValidateProcessorFactory.Create(dataset, datasetArchive);
            Assert.True(datasetValidateProcessor.IsValid);
            Assert.Empty(datasetValidateProcessor.GetErrors());
        }

        [Fact]
        public void WrongValidationDataset()
        {
            var datasetValidateProcessorFactory = _serviceProvider.GetService<IDatasetValidateProcessorFactory>();

            var dataset = new Dataset();
            var datasetArchive = new DatasetArchive();

            var datasetValidateProcessor = datasetValidateProcessorFactory.Create(dataset, datasetArchive);
            Assert.False(datasetValidateProcessor.IsValid);
            Assert.NotEmpty(datasetValidateProcessor.GetErrors());
        }
    }
}
