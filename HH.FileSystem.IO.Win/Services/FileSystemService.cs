using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using HH.FileSystem.IO.Interfaces;

namespace HH.FileSystem.IO.Win.Services
{
    [DebuggerNonUserCode]
    public class FileSystemService : IFileSystemService
    {
        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public Stream CreateFile(string fileName)
        {
            return File.Create(fileName);
        }

        public Stream OpenFile(string path)
        {
            return File.OpenRead(path);
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public void CreateDirectory(string directory)
        {
            Directory.CreateDirectory(directory);
        }

        public object ReadTextFromFile(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public string GetDirectoryName(string fileName)
        {
            return Path.GetDirectoryName(fileName);
        }

        public string GetFileName(string fileName)
        {
            return Path.GetFileName(fileName);
        }

        public string GetExension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public StreamWriter CreateText(string path)
        {
            return File.CreateText(path);
        }

        public StreamReader OpenText(string path)
        {
            return File.OpenText(path);
        }

        public string[] GetFiles(string directory, string searchPattern)
        {
            return Directory.GetFiles(directory, searchPattern);
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns><c>true</c> if successfull, <c>false</c> othervice.</returns>
        public async Task<bool> SaveFileAsync(Stream stream, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                var copyTask = stream.CopyToAsync(fileStream);
                await copyTask;
                return copyTask.Status == TaskStatus.RanToCompletion;
            }
        }

        public bool TryOpen(string location)
        {
            Stream fileStream = null;
            try
            {
                fileStream = OpenFile(location);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                fileStream?.Close();
            }
        }
    }
}