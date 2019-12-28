namespace HH.Composite.Model.Interfaces
{
    public interface ICompositeVisitor<T> where T : IComposite<T> 
    {
        void Visit(IComposite<T> node);
    }
}
