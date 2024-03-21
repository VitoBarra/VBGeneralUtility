using System.Collections.Generic;
using System.Linq;

namespace VitoBarra.GeneralUtility.DataStructure
{
    public class EdgeList<T> : List<Node<T>>
    {
        private IList<T> CachedValue;
        bool Dirty = true;

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
            Dirty = false;
            return CachedValue;
        }

        public IList<T> Values => Dirty ? UpdateCacheValue() : CachedValue;


        public void AddEdgeRange(IEnumerable<Node<T>> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                if (node == null) continue;
                Add(node);
            }

            Dirty = true;
        }

        public void AddEdgeRange(IEnumerable<T> nodes)
        {
            AddEdgeRange(nodes.Where(x => x != null).Select(x => new Node<T>(x)).ToList());
            Dirty = true;
        }

        public void AddEdge(T node)
        {
            if (node == null) return;
            Add(new Node<T>(node));
            Dirty = true;
        }

        public void AddEdge(Node<T> node)
        {
            if (node == null) return;
            Add(node);
            Dirty = true;
        }

        public void RemoveEdge(T node)
        {
            if (node == null) return;

            Remove(this.First(x => x.Value.Equals(node)));
            Dirty = true;
        }

        public void RemoveEdge(Node<T> node)
        {
            if (node == null) return;

            Remove(this.First(x => x.Value.Equals(node)));
            Dirty = true;
        }
    }
}