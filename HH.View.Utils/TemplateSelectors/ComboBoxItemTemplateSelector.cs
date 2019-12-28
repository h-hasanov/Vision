using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using HH.View.Utils.Extensions;

namespace HH.View.Utils.TemplateSelectors
{
    [DebuggerNonUserCode]
    public sealed class ComboBoxItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedTemplate { get; set; }
        public DataTemplate DropDownTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item,
            DependencyObject container)
        {
            var comboBoxItem = container.GetVisualParent<ComboBoxItem>();
            if (comboBoxItem == null)
            {
                return SelectedTemplate;
            }
            return DropDownTemplate;
        }
    }
}