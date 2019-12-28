using System.Globalization;

namespace HH.ViewModel.Services.Cultures
{
    public interface ICultureProvider
    {
        CultureInfo GetCurrentCultureInfo();
        CultureInfo[] GetAllSpecificCultureInfos();
    }
}
