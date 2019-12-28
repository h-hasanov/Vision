using System;
using System.Windows;
using System.Windows.Controls;
using HH.ErrorManager.ViewModel.Interfaces;

namespace HH.ErrorManager.View.Selectors
{
    internal sealed class ErrorStyleSelector : StyleSelector
    {
        public Style ErrorInfoContainerViewModelStyle { get; set; }
        public Style ErrorInfoViewModelStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is IErrorInfoContainerViewModel) return ErrorInfoContainerViewModelStyle;
            if (item is IErrorInfoViewModel) return ErrorInfoViewModelStyle;

            throw new NotImplementedException();
        }
    }
}
