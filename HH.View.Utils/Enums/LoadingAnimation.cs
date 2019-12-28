using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HH.View.Utils.Enums
{
    [DebuggerNonUserCode]
    public class LoadingAnimation : Control
    {
        #region IsLoading Property

        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register("IsLoading",
                                                                                                  typeof(bool),
                                                                                                  typeof(
                                                                                                      LoadingAnimation),
                                                                                                  new PropertyMetadata(
                                                                                                      false,
                                                                                                      HandleIsLoadingChanged));

        private static void HandleIsLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as LoadingAnimation;
            var loading = (bool)e.NewValue;
            if (ctrl != null)
            {
                if (loading)
                {
                    ctrl.Begin();
                }
                else
                {
                    ctrl.Stop();
                }
            }
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        #endregion

        #region AutoPlay Property

        public static readonly DependencyProperty AutoPlayProperty = DependencyProperty.Register("AutoPlay",
                                                                                                 typeof(bool),
                                                                                                 typeof(
                                                                                                     LoadingAnimation),
                                                                                                 new PropertyMetadata(
                                                                                                     default(bool),
                                                                                                     HandleAutoPlayChanged));

        private static void HandleAutoPlayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as LoadingAnimation;
            if (ctrl != null)
            {
                if ((bool)e.NewValue)
                    ctrl.Begin();
                else
                    ctrl.Stop();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the animation should play on load.
        /// </summary>
        [System.ComponentModel.Category("Loading Animation Properties"),
         System.ComponentModel.Description("Whether the animation auto plays.")]
        public bool AutoPlay
        {
            get { return (bool)GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }


        #endregion

        /// <summary>
        /// Ellipse fill property.
        /// </summary>
        public static readonly DependencyProperty EllipseFillProperty =
            DependencyProperty.Register("EllipseFill", typeof(Brush), typeof(LoadingAnimation), null);

        /// <summary>
        /// Ellipse stroke property.
        /// </summary>
        public static readonly DependencyProperty EllipseStrokeProperty =
            DependencyProperty.Register("EllipseStroke", typeof(Brush), typeof(LoadingAnimation), null);

        /// <summary>
        /// Stores the loading animation storyboard.
        /// </summary>
        private Storyboard _loadingAnimation;

        /// <summary>
        /// Stores whether the animation is running.
        /// </summary>
        private AnimationState _animationState;

        private Grid _grid;

        /// <summary>
        /// LoadingAnimation constructor.
        /// </summary>
        static LoadingAnimation()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingAnimation),
                                                     new FrameworkPropertyMetadata(typeof(LoadingAnimation)));
        }

        /// <summary>
        /// Gets or sets the ellipse fill.
        /// </summary>
        [System.ComponentModel.Category("Loading Animation Properties"),
         System.ComponentModel.Description("The fill for the little ellipses.")]
        public Brush EllipseFill
        {
            get { return (Brush)GetValue(EllipseFillProperty); }
            set { SetValue(EllipseFillProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ellipse stroke.
        /// </summary>
        [System.ComponentModel.Category("Loading Animation Properties"),
         System.ComponentModel.Description("The stroke for the little ellipses.")]
        public Brush EllipseStroke
        {
            get { return (Brush)GetValue(EllipseStrokeProperty); }
            set { SetValue(EllipseStrokeProperty, value); }
        }


        /// <summary>
        /// Gets the animation state,
        /// </summary>
        public AnimationState AnimationState
        {
            get { return _animationState; }
        }

        /// <summary>
        /// Gets the parts out of the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _loadingAnimation = (Storyboard)FindResource("PART_LoadingAnimation");
            _grid = GetTemplateChild("aminationGrid") as Grid;
            if (AutoPlay || IsLoading)
            {
                Begin();
            }
        }

        /// <summary>
        /// Begins the loading animation.
        /// </summary>
        public void Begin()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action(Begin));
                return;
            }

            if (_loadingAnimation != null)
            {
                Visibility = Visibility.Visible;
                Opacity = 1;
                _animationState = AnimationState.Playing;
                _loadingAnimation.Begin(_grid, true);
            }
        }

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public void Pause()
        {
            if (_loadingAnimation != null)
            {
                _animationState = AnimationState.Paused;
                _loadingAnimation.Pause(_grid);
            }
        }

        /// <summary>
        /// Resumes the animation.
        /// </summary>
        public void Resume()
        {
            if (_loadingAnimation != null)
            {
                Visibility = Visibility.Visible;
                _animationState = AnimationState.Playing;
                _loadingAnimation.Resume(_grid);
            }
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action(Stop));
                return;
            }

            if (_loadingAnimation != null)
            {
                _animationState = AnimationState.Stopped;
                _loadingAnimation.Stop(_grid);
                Opacity = 0;
                Dispatcher.BeginInvoke(new Func<Visibility>(() => Visibility = Visibility.Collapsed));
            }
        }
    }
    public enum AnimationState : byte
    {
        /// <summary>
        /// The animation is playing.
        /// </summary>
        Playing = 0,

        /// <summary>
        /// The animation is paused.
        /// </summary>
        Paused = 1,

        /// <summary>
        /// The animation is stopped.
        /// </summary>
        Stopped = 2
    }

}
