using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine.Serialization;

namespace VitoBarra.GeneralUtility.DataStructure
{
    public class DynamicNode<T> : Node<T>
    {
        public DynamicNode(T value) : base(value)
        {
        }


        public Func<DynamicNode<T>, IList<DynamicNode<T>>> SearchParentRule;
        public Func<DynamicNode<T>, IList<DynamicNode<T>>> SearchChildRule;

        public IList<DynamicNode<T>> SearchParents() => SearchParentRule?.Invoke(this);

        public void UpdateParent(bool notifyOld = true, bool notifyNew = true) =>
            SetAndUpdateParent(SearchParents(), notifyOld, notifyNew);

        public void UpdateChildren(bool notifyOld = true, bool notifyNew = true) =>
            SetAndUpdateChildren(SearchChildren(), notifyOld, notifyNew);

        public virtual void SetAndUpdateParent(IList<DynamicNode<T>> newParents, bool notifyOld = true,
            bool notifyNew = true)
        {
            if (newParents != null && newParents.Count != 0 && newParents.All(x => Parents.Contains(x))) return;


            if (notifyOld)
            {
                var list = new EdgeList<T>(Parents);
                for (var i = 0; i < list.Count; i++)
                {
                    var oldParent = Parents[i] as DynamicNode<T>;
                    Parents[i] = null;
                    oldParent?.UpdateChildren(false, true);
                }
            }

            Parents.Clear();
            if (newParents == null) return;

            if (notifyNew)
                foreach (var newParent in newParents)
                    newParent?.UpdateChildren(true, false);

            Parents.AddEdgeRange(newParents);
        }


        public IList<DynamicNode<T>> SearchChildren() => SearchChildRule?.Invoke(this);


        public virtual void SetAndUpdateChildren(IList<DynamicNode<T>> newChildren, bool notifyOld = true,
            bool notifyNew = true)
        {
            if (newChildren != null && newChildren.Count != 0 && newChildren.All(x => Children.Contains(x)))
                return;

            if (notifyOld)
            {
                var list = new EdgeList<T>(Children);
                for (int i = 0; i < list.Count; i++)
                {
                    var oldChild = Children[i] as DynamicNode<T>;
                    Children[i] = null;
                    oldChild?.UpdateParent(false, true);
                }
            }

            Children.Clear();

            if (newChildren == null) return;

            if (notifyNew)
                foreach (var newChild in newChildren)
                    newChild?.UpdateParent(true, false);

            Children.AddEdgeRange(newChildren);
        }

        public void DeleteNode()
        {
            var parents = new EdgeList<T>(Parents);
            foreach (var parent in parents.Where(x => x != null).Select(x => x as DynamicNode<T>))
                parent.UpdateChildren();


            var children = new EdgeList<T>(Children);
            foreach (var child in children.Where(x => x != null).Select(x => x as DynamicNode<T>))
                child.UpdateParent();
        }
    }
}