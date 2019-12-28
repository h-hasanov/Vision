using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using HH.ViewModel.Services.Interfaces;
using HH.ViewModel.Services.Services;

namespace HH.ViewModel.Services.Win.Services
{
    [DebuggerNonUserCode]
    public class ClipboardService : IClipboardService
    {
        /// <summary>
        /// Gets the size of the clip board.
        /// </summary>
        /// <returns></returns>
        public ClipboardSize GetClipBoardTextSize()
        {
            var dataRows = GetClipBoardText();
            var rows = 0;
            var columns = 0;
            foreach (var dataRow in dataRows)
            {
                columns = Math.Max(dataRow.Count(), columns);
                rows++;
            }
            return new ClipboardSize(rows, columns);
        }

        /// <summary>
        /// Gets the data from the clipboard
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string[]> GetClipBoardText()
        {
            IEnumerable<string[]> clipboardData = null;
            object clipboardRawData;
            Func<string,string[]> parseFormat = null;

            // get the data and set the parsing method based on the format
            // currently works with CSV and Text DataFormats            
            var dataObj = Clipboard.GetDataObject();
            if (dataObj == null)
                return Enumerable.Empty<string[]>();

            if ((clipboardRawData = dataObj.GetData(DataFormats.CommaSeparatedValue)) != null)
            {
                parseFormat = ParseCsvFormat;
            }
            else if ((clipboardRawData = dataObj.GetData(DataFormats.Text)) != null)
            {
                parseFormat = ParseTextFormat;
            }

            //If format is unkown this means that we do not know how to parse the text on the clipboard.
            if (parseFormat == null) throw new FormatException();

            var rawDataStr = clipboardRawData as string;

            if (rawDataStr == null && clipboardRawData is MemoryStream)
            {
                // cannot convert to a string so try a MemoryStream
                var ms = clipboardRawData as MemoryStream;
                var sr = new StreamReader(ms);
                rawDataStr = sr.ReadToEnd();
            }
            Debug.Assert(rawDataStr != null, $"clipboardRawData: {clipboardRawData}, could not be converted to a string or memorystream.");

            string[] rows = rawDataStr.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length > 0)
            {
                clipboardData = rows.Select(row => parseFormat(row));
            }
            else
            {
                Debug.WriteLine("unable to parse row data.  possibly null or contains zero rows.");
            }

            return clipboardData;
        }

        public string[] ParseCsvFormat(string value)
        {
            return ParseCsvOrTextFormat(value, ',');
        }

        public string[] ParseTextFormat(string value)
        {
            return ParseCsvOrTextFormat(value, '\t');
        }

        private static string[] ParseCsvOrTextFormat(string value, char separator)
        {
            return value.Split(separator);
        }
    }
}
