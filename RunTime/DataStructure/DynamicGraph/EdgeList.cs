using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;
using NotImplementedException = System.NotImplementedException;

namespace VitoBarra.GeneralUtility.DataStructure
{
    public class EdgeList<T> : List<Node<T>>
    {
        private IList<T> CachedValue;

        public EdgeList() : base()
        {
            CachedValue = new List<T>();
        }

        public EdgeList(EdgeList<T> edgeList) : this()
        {
            AddEdgeRange(edgeList);
        }

        private IList<T> UpdateCacheValue()
        {
            CachedValue = this.Where(x => x != null).Select(x => x.Value).ToList();
            return CachedValue;
        }

        public IList<T> Values => Count != CachedValue.Count ? UpdateCacheValue() : CachedValue;


        public void AddEdgeRange(IEnumerable<Node<T>> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                if (node == null) continue;
                Add(node);
            }

            UpdateCacheValue();
        }

        public void AddEdgeRange(IEnumerable<T> nodes)
        {
            AddEdgeRange(nodes.Where(x => x != null).Select(x => new Node<T>(x)).ToList());
        }

        public void AddEdge(T node)
        {
            if (node == null) return;
            Add(new Node<T>(node));
            UpdateCacheValue();
        }

        public void AddEdge(Node<T> node)
        {
            if (node == null) return;
            Add(node);
            UpdateCacheValue();
        }

        public void RemoveEdge(T node)
        {
            if (node == null) return;

            Remove(this.First(x => x.Value.Equals(node)));
            UpdateCacheValue();
        }

        public void RemoveEdge(Node<T> node)
        {
            if (node == null) return;

            Remove(this.First(x => x.Value.Equals(node)));
            UpdateCacheValue();
        }
    }
}