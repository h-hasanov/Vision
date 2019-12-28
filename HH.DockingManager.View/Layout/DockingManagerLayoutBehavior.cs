using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Xml;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace HH.DockingManager.View.Layout
{
    [DebuggerNonUserCode]
    public sealed class DockingManagerLayoutBehavior : Behavior<Xceed.Wpf.AvalonDock.DockingManager>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            var dm = AssociatedObject;
            RoutedEventHandler handler = null;
            handler = (s, e) =>
            {
                var settings = Settings;
                var xml = settings.Layout;
                if (!String.IsNullOrEmpty(xml))
                {
                    try
                    {
                        var xls = CreateSerializer(dm);
                        using (var sr = new StringReader(xml))
                        {
                            xls.Deserialize(sr);
                        }
                    }
                    catch
                    {
                        settings.Layout = null;
                        settings.Save();
                    }
                }
                dm.Loaded -= handler;
            };
            if (dm.IsLoaded)
            {
                handler(null, null);
            }
            else
            {
                dm.Loaded += handler;
            }
            var w = GetWindow(dm);
            if (w != null)
            {
                w.Closed += (s, e) =>
                {
                    var settings = Settings;
                    try
                    {
                        var xls = CreateSerializer(dm);
                        var sb = new StringBuilder();
                        using (var tw = new StringWriter(sb))
                        {
                            xls.Serialize(tw);
                        }
                        var removedCOnfig = DeleteLayoutDocument(sb.ToString());
                        settings.Layout = removedCOnfig;
                    }
                    catch
                    {
                        settings.Layout = null;
                    }
                    settings.Save();
                };
            }
        }

        public Window GetWindow(DependencyObject target)
        {
            return Application.Current.MainWindow;
        }

        private static XmlLayoutSerializer CreateSerializer(Xceed.Wpf.AvalonDock.DockingManager dm)
        {
            var xls = new XmlLayoutSerializer(dm);
            xls.LayoutSerializationCallback += (s, e) => e.Content = e.Content;
            return xls;
        }

        private string DeleteLayoutDocument(string avalondockConfig)
        {
            var configDoc = new XmlDocument();
            configDoc.LoadXml(avalondockConfig);
            var projectNodes = configDoc.GetElementsByTagName("LayoutDocument");
            for (var i = projectNodes.Count - 1; i > -1; i--)
            {
                projectNodes[i].ParentNode.RemoveChild(projectNodes[i]);
            }
           //configDoc.Save(avalondockConfig);
            var res = configDoc.InnerXml;
            return res;
        }

        private Properties.Settings _settings;

        private Properties.Settings Settings
        {
            get { return _settings ?? (_settings = Properties.Settings.Default); }
        }
    }
}