using System.IO;
using System.Threading.Tasks;

namespace HH.FileSystem.IO.Interfaces
{
    public interface IFileSystemService
    {
        bool FileExists(string fileName);
        Stream CreateFile(string fileName);
        Stream OpenFile(string path);
        bool DirectoryExists(string directoryPath);
        void CreateDirectory(string directory);
        object ReadTextFromFile(string fileName);
        string GetDirectoryName(string fileName);
        string GetFileName(string fileName);
        string GetExension(string fileName);
        StreamWriter CreateText(string path);

        StreamReader OpenText(string path);
        string[] GetFiles(string directory, string searchPattern);
        Task<bool> SaveFileAsync(Stream stream, string filePath);
        bool TryOpen(string location);
    }
}