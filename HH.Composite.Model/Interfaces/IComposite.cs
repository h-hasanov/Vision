using System.Collections.Generic;
using HH.Data.Entity.Model.Interfaces;
using HH.Data.Filter.Interfaces;

namespace HH.Composite.Model.Interfaces
{
    public interface IComposite<T> : IEntity where T : IComposite<T>
    {
        #region Properties

        /// <summary>
        /// Gets whether there are any children.
        /// </summary>
        bool HasChildren { get; }

        /// <summary>
        /// Gets the depth. 0 if root.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// Gets the children.
        /// </summary>
        IReadOnlyEntityCollection<T> Children { get; }

        /// <summary>
        /// Gets the parent node. Null if the current node is root.
        /// </summary>
        T Parent { get; }

        /// <summary>
        /// Gets or sets whether the node is read-only.
        /// </summary>
        bool IsReadOnly { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a child.
        /// </summary>
        /// <param name="newItem"></param>
        void AddChild(T newItem);

        /// <summary>
        /// Returns whether the child can be added.
        /// </summary>
        /// <returns></returns>
        bool CanAddChild(T newItem);

        /// <summary>
        /// Removes all children and removes from parent.
        /// </summary>
        void Remove();

        /// <summary>
        /// Returns whether can remove all children and remove from parent.
        /// </summary>
        /// <returns></returns>
        bool CanRemove();

        /// <summary>
        /// Removes the child at the specified index.
        /// </summary>
        /// <param name="index"></param>
        void RemoveChildAt(int index);

        /// <summary>
        /// Removes whether the child at the specified index can be removed.
        /// </summary>
        /// <param name="index"></param>
        bool CanRemoveChildAt(int index);

        /// <summary>
        /// Removes the given child.
        /// </summary>
        /// <param name="child"></param>
        void RemoveChild(T child);

        /// <summary>
        /// Returns whether the given child can be removed.
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        bool CanRemoveChild(T child);

        /// <summary>
        /// Removes all children.
        /// </summary>
        void RemoveChildren();

        /// <summary>
        /// Returns whether all children can be removed.
        /// </summary>
        bool CanRemoveChildren();

        /// <summary>
        /// Sets the parent object.
        /// </summary>
        /// <param name="parent"></param>
        void SetParent(T parent);

        /// <summary>
        /// This is the visitor pattern.
        /// Allows external users to pass a visitor that can get specific information.
        /// http://www.tutorialspoint.com/design_pattern/design_pattern_quick_guide.htm
        /// </summary>
        /// <param name="visitor"></param>
        void Accept(ICompositeVisitor<T> visitor);

        #endregion

        #region Searching

        IEnumerable<T> Search(ICriteria<T> searchCriteria);

        #endregion
    }
}
