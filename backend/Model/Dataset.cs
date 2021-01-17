namespace backend.Model
{
    using System;
    using System.Text.Json.Serialization;

    public class Dataset
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public bool ContainsCyrillic { get; set; }

        public bool ContainsLatin { get; set; }

        public bool ContainsNumbers { get; set; }

        public bool ContainsSpecialCharacters { get; set; }

        public bool CaseSensitivity { get; set; }

        public string LocationOfResponsesToPictures { get; set; }

        [JsonIgnore]
        public string ArchiveOriginalName { get; set; }

        [JsonIgnore]
        public string ArchiveRepositoryName { get; set; }
    }
}
