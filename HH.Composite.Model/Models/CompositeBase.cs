using System;
using System.Collections.Generic;
using HH.Composite.Model.Interfaces;
using HH.Data.Entity.Model.Entity;
using HH.Data.Entity.Model.Interfaces;
using HH.Data.Entity.Model.Services;
using HH.Data.Filter.Interfaces;
using HH.EnvironmentServices.Utils;

namespace HH.Composite.Model.Models
{
    public abstract class CompositeBase<T> : EntityBase, IComposite<T> where T : class, IComposite<T>
    {
        #region Fields

        private const string ErrorMessage = "Null tree node or a tree with non-null Parent can't be added as a child";
        protected readonly IEntityCollection<T> ChildItems;
        private bool _isReadOnly;

        #endregion

        #region Constructors

        protected CompositeBase()
            : this(StaticServices.DefaultEntityCollectionFactory)
        {

        }

        protected CompositeBase(IEntityCollectionFactory entityCollectionFactory)
        {
            entityCollectionFactory.ArgumentNullCheck("entityCollectionFactory");
            ChildItems = entityCollectionFactory.CreateEntityCollection<T>(this);

            TrackChildren(ChildItems);
        }

        #endregion

        #region Properties

        public bool HasChildren
        {
            get { return ChildItems.Count > 0; }
        }

        public int Depth
        {
            get
            {
                if (Parent == null)
                    return 0;
                return Parent.Depth + 1;
            }
        }

        public IReadOnlyEntityCollection<T> Children
        {
            get { return ChildItems; }
        }

        public T Parent { get; protected set; }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetProperty(ref _isReadOnly, value); }
        }

        protected abstract T This { get; }

        public virtual T NullT { get { return default(T); } }

        #endregion

        #region Methods

        #region AddChild

        public virtual void AddChild(T newItem)
        {
            if (!CanAddChild(newItem))
                return;

            newItem.SetParent(This);
            ChildItems.Add(newItem);
        }

        public virtual bool CanAddChild(T newItem)
        {
            if (IsReadOnly)
                return false;

            if (newItem == null || newItem.Parent != null)
                return false;
            
            return true;
        }

        #endregion

        #region Remove

        public virtual void Remove()
        {
            if (!CanRemove())
                return;

            if (Parent != null)
            {
                Parent.RemoveChild(This);
            }
        }

        public virtual bool CanRemove()
        {
            return !IsReadOnly;
        }

        #endregion

        #region RemoveChildAt

        public virtual void RemoveChildAt(int index)
        {
            if (!CanRemoveChildAt(index))
                return;

            RemoveChild(ChildItems[index]);
        }

        public virtual bool CanRemoveChildAt(int index)
        {
            if (IsReadOnly)
                return false;

            if (index >= ChildItems.Count || index < 0)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region RemoveChild

        public virtual void RemoveChild(T child)
        {
            if (!CanRemoveChild(child))
                return;

            child.RemoveChildren();
            ChildItems.Remove(child);
            child.SetParent(NullT);
            child.Dispose();
        }

        public virtual bool CanRemoveChild(T child)
        {
            if (IsReadOnly)
                return false;

            if (child == null)
                return false;

            if (!ChildItems.Contains(child))
                return false;

            return true;
        }

        #endregion

        #region RemoveChildren

        public virtual void RemoveChildren()
        {
            if (!CanRemoveChildren())
                return;

            var childrenCount = ChildItems.Count;
            for (var i = 0; i < childrenCount; i++)
            {
                var childAtIndex = ChildItems[0];
                RemoveChild(childAtIndex);
            }
        }

        public virtual bool CanRemoveChildren()
        {
            return !IsReadOnly;
        }

        #endregion

        public virtual void SetParent(T parent)
        {
            Parent = parent;
        }

        public void Accept(ICompositeVisitor<T> visitor)
        {
            visitor.Visit(This);
            ForeachChild(visitor.Visit);
        }

        #endregion

        #region Searching

        public IEnumerable<T> Search(ICriteria<T> searchCriteria)
        {
            foreach (var meetCriterion in searchCriteria.MeetCriteria(Children))
            {
                yield return meetCriterion;
            }

            foreach (var viewModel in Children)
            {
                foreach (var model in viewModel.Search(searchCriteria))
                {
                    yield return model;
                }
            }
        }

        #endregion

        #region Helpers

        protected void ForeachChild(Action<T> action)
        {
            foreach (var child in Children)
            {
                action(child);
            }
        }

        #endregion

        #region Dispose

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            if (ChildItems != null)
                ChildItems.Dispose();
        }

        #endregion
    }
}
