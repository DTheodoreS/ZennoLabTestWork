namespace backend.Repository
{
    using System.IO;

    public interface IArchiveRepository
    {
        void Save(string filename, MemoryStream stream);

        string GenerateUniqueName();
    }
}
