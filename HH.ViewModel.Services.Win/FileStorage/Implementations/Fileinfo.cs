using System.IO;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Services.FileStorage.Interfaces;

namespace HH.ViewModel.Services.Win.FileStorage.Implementations
{
    internal sealed class Fileinfo : IFileInfo
    {
        private readonly FileInfo _source;

        public Fileinfo(FileInfo source)
        {
            _source = source.ArgumentNullCheck(nameof(source));
        }

        public string FullName { get { return _source.FullName; } }
    }
}
