using System.Collections.Generic;
using System.Linq;

namespace VitoBarra.GeneralUtility.DataStructure
{
    public class GraphNode<T>
    {
        public readonly IList<GraphNode<T>> ChildComponents = new List<GraphNode<T>>();
        public int ChildCount => ChildComponents.Count;

        public T Value { get; set; }

        public GraphNode(T value)
        {
            Value = value;
        }

        public void AddChildren(IList<T> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                if (node == null) continue;
                ChildComponents.Add(new GraphNode<T>(node));
            }
        }

        public void AddChildren(IList<GraphNode<T>> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                if (node == null) continue;
                ChildComponents.Add(node);
            }
        }

        public void AddChildren(T node)
        {
            if (node == null) return;

            ChildComponents.Add(new GraphNode<T>(node));
        }


        public void AddChildren(GraphNode<T> node)
        {
            if (node == null) return;

            ChildComponents.Add(node);
        }

        public void RemoveChildren(T node)
        {
            if (node == null) return;

            ChildComponents.Remove(ChildComponents.First(x => x.Value.Equals(node)));
        }

        public void ClearChildren()
        {
            ChildComponents.Clear();
        }

        public IList<GraphNode<T>> GetTail()
        {
            if (ChildComponents.Count == 0)
                return new List<GraphNode<T>>() { this };

            var tails = new List<GraphNode<T>>();
            foreach (var child in ChildComponents)
            {
                tails.AddRange(child.GetTail());
            }

            return tails;
        }
    }
}