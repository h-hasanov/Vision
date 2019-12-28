using System.Collections.Generic;
using HH.ViewModel.Services.StandardDialog.Enums;

namespace HH.ViewModel.Services.Win.StandardDialog.Converters
{
    internal static class MessageBoxImageConverter
    {
        private static readonly IDictionary<MessageBoxImage, System.Windows.MessageBoxImage> Mapper = new Dictionary
            <MessageBoxImage, System.Windows.MessageBoxImage>
        {
            {MessageBoxImage.None, System.Windows.MessageBoxImage.None},
            {MessageBoxImage.Hand, System.Windows.MessageBoxImage.Hand},
            {MessageBoxImage.Stop, System.Windows.MessageBoxImage.Stop},
            {MessageBoxImage.Error, System.Windows.MessageBoxImage.Error},
            {MessageBoxImage.Question, System.Windows.MessageBoxImage.Question},
            {MessageBoxImage.Exclamation, System.Windows.MessageBoxImage.Exclamation},
            {MessageBoxImage.Warning, System.Windows.MessageBoxImage.Warning},
            {MessageBoxImage.Asterisk, System.Windows.MessageBoxImage.Asterisk},
            {MessageBoxImage.Information, System.Windows.MessageBoxImage.Information}
        };

        public static System.Windows.MessageBoxImage Convert(this MessageBoxImage messageBoxButton)
        {
            return Mapper[messageBoxButton];
        }
    }
}
