using System.Collections.Generic;

namespace VitoBarra.GeneralUtility.DataStructure
{
    public class Node<T>
    {
        public EdgeList<T> Children { get; private set; } = new EdgeList<T>();
        public EdgeList<T> Parents { get; private set; } = new EdgeList<T>();

        public int ChildrenCount => Children.Count;
        public int ParentsCount => Parents.Count;

        public T Value { get; set; }

        public Node(T value)
        {
            Value = value;
        }


        public void ClearAll()
        {
            Parents.Clear();
            Children.Clear();
        }

        public EdgeList<T> GetTail()
        {
            if (Children.Count == 0)
                return new EdgeList<T>() { this };

            var tails = new EdgeList<T>();
            foreach (var child in Children)
            {
                tails.AddRange(child.GetTail());
            }

            return tails;
        }
    }
}