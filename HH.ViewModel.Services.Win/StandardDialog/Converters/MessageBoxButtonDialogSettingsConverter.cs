using System;
using HH.ViewModel.Services.StandardDialog.Enums;
using MahApps.Metro.Controls.Dialogs;

namespace HH.ViewModel.Services.Win.StandardDialog.Converters
{
    internal static class MessageBoxButtonDialogSettingsConverter
    {
        public static MessageBoxResult ConvertToMessageBoxResult(this MessageDialogResult dialogResult, MessageBoxButton messageBoxButton)
        {
            switch (messageBoxButton)
            {
                case MessageBoxButton.Ok:
                    return MessageBoxResult.OK;
                case MessageBoxButton.OkCancel:
                    return dialogResult == MessageDialogResult.Affirmative ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                case MessageBoxButton.YesNoCancel:
                    {
                        if (dialogResult == MessageDialogResult.Affirmative)
                            return MessageBoxResult.Yes;
                        if (dialogResult == MessageDialogResult.Negative)
                            return MessageBoxResult.No;
                        return MessageBoxResult.Cancel;
                    }
                case MessageBoxButton.YesNo:
                    return dialogResult == MessageDialogResult.Affirmative ? MessageBoxResult.Yes : MessageBoxResult.No;
                default:
                    throw new ArgumentOutOfRangeException(nameof(messageBoxButton), messageBoxButton, null);
            }
        }

        public static Tuple<MessageDialogStyle, MetroDialogSettings> CreateDialogSettings(this MessageBoxButton button)
        {
            var dialogSettings = new MetroDialogSettings();
            var dialogStyle = MessageDialogStyle.Affirmative;

            switch (button)
            {
                case MessageBoxButton.Ok:
                    {
                        dialogSettings.AffirmativeButtonText = Resources.Ok;
                        break;
                    }
                case MessageBoxButton.OkCancel:
                    {
                        dialogSettings.AffirmativeButtonText = Resources.Ok;
                        dialogSettings.NegativeButtonText = Resources.Cancel;
                        dialogStyle = MessageDialogStyle.AffirmativeAndNegative;
                        break;
                    }
                case MessageBoxButton.YesNoCancel:
                    {
                        dialogSettings.AffirmativeButtonText = Resources.Yes;
                        dialogSettings.NegativeButtonText = Resources.No;
                        dialogSettings.FirstAuxiliaryButtonText = Resources.Cancel;
                        dialogStyle = MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary;
                        break;
                    }
                case MessageBoxButton.YesNo:
                    {
                        dialogSettings.AffirmativeButtonText = Resources.Yes;
                        dialogSettings.NegativeButtonText = Resources.No;
                        dialogStyle = MessageDialogStyle.AffirmativeAndNegative;
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
            return new Tuple<MessageDialogStyle, MetroDialogSettings>(dialogStyle, dialogSettings);
        }
    }
}
