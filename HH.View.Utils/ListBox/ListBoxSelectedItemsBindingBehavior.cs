using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interactivity;

namespace HH.View.Utils.ListBox
{
    [DebuggerNonUserCode]
    public class ListBoxSelectedItemsBindingBehavior : Behavior<System.Windows.Controls.ListBox>
    {
        public static readonly DependencyProperty SelectedItemsProperty =
     DependencyProperty.Register("SelectedItems", typeof(IList),
         typeof(ListBoxSelectedItemsBindingBehavior),
         new PropertyMetadata(OnSelectedItemsPropertyChanged));

        private static void OnSelectedItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var collection = e.NewValue as INotifyCollectionChanged;
            if (collection != null)
            {
                d.SetValue(SelectedItemsProperty, collection);
            }
        }

        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }

        private void AssociatedObject_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach (var removedItem in e.RemovedItems)
            {
                SelectedItems.Remove(removedItem);
            }

            foreach (var addedItem in e.AddedItems)
            {
                SelectedItems.Add(addedItem);
            }
        }
    }
}
