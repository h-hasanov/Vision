using System.Collections.Generic;
using System.Text;
using HH.ViewModel.Services.StandardDialog.Implementations;

namespace HH.ViewModel.Services.Win.StandardDialog.Converters
{
    public static class FileTypeFilterToStringConverter
    {
        public static string ConvertToStringFilter(this IEnumerable<FileTypeFilter> filters)
        {
            var stringBuilder = new StringBuilder();
            foreach (var fileTypeFilter in filters)
            {
                stringBuilder.Append(fileTypeFilter.ConvertToStringFilter());
                stringBuilder.Append("|");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        public static string ConvertToStringFilter(this FileTypeFilter fileTypeFilter)
        {
            return string.Format("{0} files (*{1})|*{1}", fileTypeFilter.Description, fileTypeFilter.Extension);
        }
    }
}
