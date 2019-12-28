using System.Diagnostics;
using System.Globalization;
using HH.ViewModel.Services.Cultures;

namespace HH.ViewModel.Services.Win.Cultures
{
    [DebuggerNonUserCode]
    public sealed class CultureProvider : ICultureProvider
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            return CultureInfo.CurrentCulture;
        }

        public CultureInfo[] GetAllSpecificCultureInfos()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures);
        }
    }
}
