namespace HH.ViewModel.Services.StandardDialog.Implementations
{
    public struct FileTypeFilter
    {
        public FileTypeFilter(string extension, string description)
        {
            Extension = extension;
            Description = description;
        }

        public string Extension { get; }
        public string Description { get; }
    }
}
