using System.Collections.Generic;
using System.Linq;

namespace VitoBarra.GeneralUtility.DataStructure
{
    public enum LinkDirection
    {
        Outgoing,
        Incoming
    }
    public class GraphNode<T>
    {
        public readonly IList<GraphNode<T>> Outgoing = new List<GraphNode<T>>();
        public readonly IList<GraphNode<T>> Incoming = new List<GraphNode<T>>();
        public int OutgoingCount => Outgoing.Count;
        public int IncomingCount => Outgoing.Count;

        public T Value { get; set; }

        public GraphNode(T value)
        {
            Value = value;
        }

        public IList<GraphNode<T>> DirectionSelector(LinkDirection direction) => direction == LinkDirection.Outgoing ? Outgoing : Incoming;

        public void AddLink(LinkDirection linkDirection,IList<T> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                if (node == null) continue;
                DirectionSelector(linkDirection).Add(new GraphNode<T>(node));
            }
        }

        public void AddLink(LinkDirection linkDirection,IList<GraphNode<T>> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                if (node == null) continue;
                DirectionSelector(linkDirection).Add(node);
            }
        }

        public void AddLink(LinkDirection linkDirection,T node)
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

            DirectionSelector(linkDirection).Remove(Outgoing.First(x => x.Value.Equals(node)));
        }

        public void ClearLink(LinkDirection linkDirection)
        {
            DirectionSelector(linkDirection).Clear();
        }

        public IList<GraphNode<T>> GetLink(LinkDirection linkDirection)
        {
            return DirectionSelector(linkDirection);
        }

        public void ClearAll()
        {
            Incoming.Clear();
            Outgoing.Clear();
        }

        public IList<GraphNode<T>> GetTail()
        {
            if (Outgoing.Count == 0)
                return new List<GraphNode<T>>() { this };

            var tails = new List<GraphNode<T>>();
            foreach (var child in Outgoing)
            {
                tails.AddRange(child.GetTail());
            }

            return tails;
        }
    }
}