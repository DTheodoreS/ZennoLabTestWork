namespace backend.tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using backend;
    using backend.DatasetValidation;

    using Newtonsoft.Json;

    using Xunit;

    public class DatasetControllerTests : IClassFixture<RealFixture<Startup>>
    {
        private readonly string _datasetControllerUrl = "/api/dataset";

        private readonly HttpClient _client;

        public DatasetControllerTests(RealFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GetAllDatasetList()
        {
            var response = await _client.GetAsync(_datasetControllerUrl);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task WrongUploadDataset()
        {
            var multiForm = new MultipartFormDataContent();

            var dataset = new Dictionary<string, string>
            {
                { "name", "tes1" },
                { "date", DateTime.Now.ToString() },
            };
            AppendDataToFormData(dataset, multiForm);

            var response = await _client.PostAsync(_datasetControllerUrl, multiForm);

            var value = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DatasetValidateResult>>(value);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task SuccessUploadDataset()
        {
            var multiForm = new MultipartFormDataContent();

            var dataset = new Dictionary<string, string>
            {
                { "name", "testing" },
                { "date", DateTime.Now.ToString() },
                { "ContainsLatin", "true" },
                { "ContainsNumbers", "true" },
                { "ContainsSpecialCharacters", "true" },
            };
            AppendDataToFormData(dataset, multiForm);

            HttpResponseMessage response;
            using (MemoryStream ms = new MemoryStream(Properties.Resources.oknew))
            {
                multiForm.Add(new StreamContent(ms), "file", "oknew.zip");
                response = await _client.PostAsync(_datasetControllerUrl, multiForm);
            }

            var value = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DatasetValidateResult>>(value);
            Assert.Empty(result);
        }

        private void AppendDataToFormData(Dictionary<string, string> data, MultipartFormDataContent form)
        {
            foreach (var kvp in data)
            {
                form.Add(new StringContent(kvp.Value), String.Format("\"{0}\"", kvp.Key));
            }
        }
    }
}
