using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace HH.View.Utils.RichTextBox
{
    public static class RichTextBoxRtfAttachedProperty
    {
        public static readonly DependencyProperty RtfProperty =
          DependencyProperty.RegisterAttached(
              "Rtf",
              typeof(string),
              typeof(RichTextBoxRtfAttachedProperty),
              new PropertyMetadata(OnRtfChanged));

        public static string GetRtf(DependencyObject target)
        {
            return (string)target.GetValue(RtfProperty);
        }

        public static void SetRtf(DependencyObject target, string value)
        {
            target.SetValue(RtfProperty, value);
        }

        private static void OnRtfChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
            var rtb = (System.Windows.Controls.RichTextBox) d;
            rtb.Document.Blocks.Clear();// Delete existing text
            var inputString = (string) e.NewValue;
            if (!string.IsNullOrEmpty(inputString))
            {
                var stream = new MemoryStream(Encoding.Default.GetBytes(inputString));
                rtb.Selection.Load(stream, DataFormats.Rtf);
            }
        }
    }
}