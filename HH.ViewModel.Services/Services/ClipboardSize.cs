using System;

namespace HH.ViewModel.Services.Services
{
    public struct ClipboardSize
    {
        public ClipboardSize(int numberOfRows, int numberOfColumns)
        {
            if (numberOfRows < 0 || numberOfColumns < 0)
                throw new ArgumentException();

            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
        }

        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }
    }
}