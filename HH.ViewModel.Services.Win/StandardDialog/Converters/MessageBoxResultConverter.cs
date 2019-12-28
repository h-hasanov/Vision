using System.Collections.Generic;
using System.Linq;
using HH.ViewModel.Services.StandardDialog.Enums;

namespace HH.ViewModel.Services.Win.StandardDialog.Converters
{
    internal static class MessageBoxResultConverter
    {
        private static readonly IDictionary<MessageBoxResult, System.Windows.MessageBoxResult> Mapper = new Dictionary
          <MessageBoxResult, System.Windows.MessageBoxResult>
        {
            {MessageBoxResult.None, System.Windows.MessageBoxResult.None},
            {MessageBoxResult.OK, System.Windows.MessageBoxResult.OK},
            {MessageBoxResult.Cancel, System.Windows.MessageBoxResult.Cancel},
            {MessageBoxResult.Yes, System.Windows.MessageBoxResult.Yes},
            {MessageBoxResult.No, System.Windows.MessageBoxResult.No},
        };

        public static System.Windows.MessageBoxResult Convert(this MessageBoxResult messageBoxButton)
        {
            return Mapper[messageBoxButton];
        }

        public static MessageBoxResult Convert(this System.Windows.MessageBoxResult messageBoxButton)
        {
            return Mapper.First(c => c.Value == messageBoxButton).Key;
        }
    }
}
