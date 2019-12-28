using System.Collections.Generic;
using HH.ViewModel.Services.StandardDialog.Enums;

namespace HH.ViewModel.Services.Win.StandardDialog.Converters
{
    internal static class MessageBoxButtonConverter
    {
        private static readonly IDictionary<MessageBoxButton, System.Windows.MessageBoxButton> Mapper = new Dictionary
            <MessageBoxButton, System.Windows.MessageBoxButton>
        {
            {MessageBoxButton.Ok, System.Windows.MessageBoxButton.OK},
            {MessageBoxButton.OkCancel, System.Windows.MessageBoxButton.OKCancel},
            {MessageBoxButton.YesNoCancel, System.Windows.MessageBoxButton.YesNoCancel},
            {MessageBoxButton.YesNo, System.Windows.MessageBoxButton.YesNo}
        };

        public static System.Windows.MessageBoxButton Convert(this MessageBoxButton messageBoxButton)
        {
            return Mapper[messageBoxButton];
        }
    }
}
