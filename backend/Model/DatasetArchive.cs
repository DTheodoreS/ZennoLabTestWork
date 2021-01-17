namespace backend.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;

    public class DatasetArchive
    {
        public DatasetArchive()
        {
        }

        public DatasetArchive(MemoryStream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            try
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Read, true))
                {
                    Files = archive.Entries
                        .Select(e => e.Name)
                        .ToList();

                    ZipArchiveEntry answersEntry = archive.GetEntry("answers.txt");
                    if (answersEntry != null)
                    {
                        ExistAnswers = true;
                        var answerEntryList = ParseAnswerEntryList(answersEntry);
                        answerEntryList
                            .ToList()
                            .ForEach(ParseAnswerEntry);
                    }
                }
            }
            catch
            {
                WrongArchive = true;
            }
        }

        public readonly List<string> Files = new List<string>();

        public bool WrongArchive;

        public bool ExistAnswers;

        public readonly Dictionary<string,string> AnswerEntryList = new Dictionary<string, string>();

        public bool ExistWrongAnswerEntry;

        public readonly List<string> WrongAnswerEntryList = new List<string>();

        private string[] ParseAnswerEntryList(ZipArchiveEntry answersEntry)
        {
            using (StreamReader reader = new StreamReader(answersEntry.Open()))
            {
                var text = reader.ReadToEnd();
                var separator = new char[] { (char)10, (char)13 };
                var answerEntryList = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                return answerEntryList;
            }
        }

        private void ParseAnswerEntry(string answerEntry)
        {
            if (String.IsNullOrEmpty(answerEntry))
            {
                ExistWrongAnswerEntry = true;
                WrongAnswerEntryList.Add(answerEntry);
                return;
            }

            var separator = new char[] { ':' };
            var answerEntryList = answerEntry.Split(separator);
            if (answerEntryList.Length != 2
                || String.IsNullOrEmpty(answerEntryList[0])
                || String.IsNullOrEmpty(answerEntryList[1]))
            {
                ExistWrongAnswerEntry = true;
                WrongAnswerEntryList.Add(answerEntry);
                return;
            }

            AnswerEntryList.Add(answerEntryList[0], answerEntryList[1]);
        }
    }
}
