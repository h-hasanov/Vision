namespace HH.DockingManager.ViewModel.Interfaces
{
    public interface IPaneSettings : IDockableViewSettings
    {
        string Title { get; set; }
        string ContentId { get; set; }
        string ParentPaneName { get; set; }
    }
}
