using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace VitoBarra.GeneralUtility.DataStructure
{
    public enum LinkDirection
    {
        Children,
        Parent
    }

    public class GraphNode<T>
    {
        public readonly IList<GraphNode<T>> Children = new List<GraphNode<T>>();
        public readonly IList<GraphNode<T>> Parents = new List<GraphNode<T>>();

        public int ChildrenCount => Children.Count;
        public int ParentsCount => Parents.Count;

        public T Value { get; set; }

        public GraphNode(T value)
        {
            Value = value;
        }

        private IList<GraphNode<T>> DirectionSelector(LinkDirection direction) =>
            direction == LinkDirection.Children ? Children : Parents;

        public void AddLink(LinkDirection linkDirection, IList<T> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                if (node == null) continue;
                DirectionSelector(linkDirection).Add(new GraphNode<T>(node));
            }
        }

        public void AddLink(LinkDirection linkDirection, IList<GraphNode<T>> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                if (node == null) continue;
                DirectionSelector(linkDirection).Add(node);
            }
        }

        public void AddLink(LinkDirection linkDirection, T node)
        {
            if (node == null) return;

            DirectionSelector(linkDirection).Add(new GraphNode<T>(node));
        }


        public void AddLink(LinkDirection linkDirection, GraphNode<T> node)
        {
            if (node == null) return;

            DirectionSelector(linkDirection).Add(node);
        }

        public void RemoveLink(LinkDirection linkDirection, T node)
        {
            if (node == null) return;

            DirectionSelector(linkDirection).Remove(Children.First(x => x.Value.Equals(node)));
        }

        public void ClearLink(LinkDirection linkDirection)
        {
            DirectionSelector(linkDirection).Clear();
        }


        public void ClearAll()
        {
            Parents.Clear();
            Children.Clear();
        }

        public IList<GraphNode<T>> GetTail()
        {
            if (Children.Count == 0)
                return new List<GraphNode<T>>() { this };

            var tails = new List<GraphNode<T>>();
            foreach (var child in Children)
            {
                tails.AddRange(child.GetTail());
            }

            return tails;
        }

        public Func<GraphNode<T>, IList<GraphNode<T>>> SearchParent;
        public Func<GraphNode<T>, IList<GraphNode<T>>> SearchChild;


        public void UpdateParent(bool notifyOld = true, bool notifyNew = true) =>
            SetAndUpdateParent(SearchParent?.Invoke(this), notifyOld, notifyNew);

        public void SetAndUpdateParent(IList<GraphNode<T>> newParents, bool notifyOld = true, bool notifyNew = true)
        {
            if (newParents != null && newParents.Count != 0 && newParents.All(x => Parents.Contains(x))) return;


            if (notifyOld)
            {
                var list = new List<GraphNode<T>>(Parents);
                for (var i = 0; i < list.Count; i++)
                {
                    var oldParent = Parents[i];
                    Parents[i] = null;
                    oldParent?.UpdateChildren(false, true);
                }
            }

            ClearLink(LinkDirection.Parent);
            if (newParents == null) return;

            if (notifyNew)
                foreach (var newParent in newParents)
                    newParent?.UpdateChildren(true, false);


            AddLink(LinkDirection.Parent, newParents.ToList());
        }


        public void UpdateChildren(bool notifyOld = true, bool notifyNew = true) =>
            SetAndUpdateChildren(SearchChild?.Invoke(this), notifyOld, notifyNew);

        public void SetAndUpdateChildren(IList<GraphNode<T>> newChildren, bool notifyOld = true, bool notifyNew = true)
        {
            if (newChildren != null && newChildren.Count != 0 && newChildren.All(x => Children.Contains(x)))
                return;

            if (notifyOld)
            {
                var list = new List<GraphNode<T>>(Children);
                for (int i = 0; i < list.Count; i++)
                {
                    var oldChild = Children[i];
                    Children[i] = null;
                    oldChild?.UpdateParent(false, true);
                }
            }

            ClearLink(LinkDirection.Children);

            if (newChildren == null) return;

            if (notifyNew)
                foreach (var newChild in newChildren)
                    newChild?.UpdateParent(true, false);

            AddLink(LinkDirection.Children, newChildren.ToList());
        }

        public void DeleteNode()
        {
            var parents = new List<GraphNode<T>>(Parents);
            foreach (var parent in parents.Where(x => x != null))
                parent.UpdateChildren();


            var children = new List<GraphNode<T>>(Children);
            foreach (var child in children.Where(x => x != null))
                child.UpdateParent();
        }
    }
}