using System.Collections.Generic;
using HH.ViewModel.Services.Services;

namespace HH.ViewModel.Services.Interfaces
{
    public interface IClipboardService
    {
        ClipboardSize GetClipBoardTextSize();
        IEnumerable<string[]> GetClipBoardText();
    }
}
