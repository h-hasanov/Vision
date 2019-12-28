using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace HH.View.Utils.Behaviors
{
    [DebuggerNonUserCode]
    public sealed class ComboBoxItemsUpdateOnDropDownBehavior : Behavior<ComboBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.DropDownOpened += AssociatedObject_DropDownOpened;
        }

        private void AssociatedObject_DropDownOpened(object sender, EventArgs e)
        {
            ((ComboBox)sender).GetBindingExpression(ComboBox.ItemsSourceProperty)
                        .UpdateTarget();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null)
            {
                AssociatedObject.DropDownOpened -= AssociatedObject_DropDownOpened;
            }
        }
    }
}
