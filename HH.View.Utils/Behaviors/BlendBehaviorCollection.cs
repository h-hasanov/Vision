using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interactivity;

namespace HH.View.Utils.Behaviors
{
    [DebuggerNonUserCode]
    public static class BlendBehaviorCollection
    {
        public static readonly DependencyProperty BehaviorsProperty =
            DependencyProperty.RegisterAttached("Behaviors", typeof(Behaviors), typeof(BlendBehaviorCollection),
            new UIPropertyMetadata(null, OnBehaviorsChanged));

        private static void OnBehaviorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behaviors = Interaction.GetBehaviors(d);
            foreach (var behavior in (Behaviors)e.NewValue)
            {
                behaviors.Add(behavior);
            }
        }

        public static Behaviors GetBehaviors(DependencyObject obj)
        {
            return (Behaviors)obj.GetValue(BehaviorsProperty);
        }

        public static void SetBehaviors(DependencyObject obj, Behaviors value)
        {
            obj.SetValue(BehaviorsProperty, value);
        }
    }

    public class Behaviors : List<Behavior>
    { }
}