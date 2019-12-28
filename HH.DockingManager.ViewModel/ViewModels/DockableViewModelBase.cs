using HH.DockingManager.ViewModel.Interfaces;
using HH.Icons.Model.Enums;
using HH.ViewModel.ViewModels;

namespace HH.DockingManager.ViewModel.ViewModels
{
    internal abstract class DockableViewModelBase : ViewModelBase, IDockableViewModel
    {
        private string _title;
        private bool _isVisible;
        private bool _isSelected;
        private bool _isActive;
        private string _toolTip;
        private GlyphType _iconSource;

        protected DockableViewModelBase()
        {
            IsVisible = true;
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        public string ToolTip
        {
            get { return _toolTip; }
            protected set { SetProperty(ref _toolTip, value); }
        }

        public GlyphType IconSource
        {
            get { return _iconSource; }
            protected set { SetProperty(ref _iconSource, value); }
        }

    }
}
