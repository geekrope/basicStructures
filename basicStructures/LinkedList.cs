using System;
using System.Text;

namespace basicStructures
{
    public class LinkedList<T>
    {
        private LinkedListNode<T>? first;

        public LinkedListNode<T>? First
        {
            get { return first; }
        }
        public int Count
        {
            get
            {
                int count = 0;

                ForEach((LinkedListNode<T> node, int index) => { count++; });

                return count;
            }
        }
        public LinkedListNode<T>? Last
        {
            get
            {
                LinkedListNode<T>? output = null;

                ForEach((LinkedListNode<T> node, int index) =>
                {
                    output = node;
                });

                return output;
            }
        }

        public string Print()
        {
            StringBuilder output = new();

            ForEach((LinkedListNode<T> node, int index) => { output.Append(node.Value?.ToString() + " "); });

            return output.ToString();
        }
        public LinkedListNode<T>? Find(T key)
        {
            LinkedListNode<T>? output = null;

            ForEach((LinkedListNode<T> node, int index) =>
            {
                if (output == null && node.Value.Equals(key))
                {
                    output = node;
                }
            });

            return output;
        }
        public LinkedListNode<T>? FindByIndex(int index)
        {
            LinkedListNode<T>? output = null;

            ForEach((LinkedListNode<T> node, int currentIndex) =>
            {
                if (output == null && currentIndex == index)
                {
                    output = node;
                }
            });

            return output;
        }
        public LinkedListNode<T>? FindLast(T key)
        {
            LinkedListNode<T>? output = null;

            ForEach((LinkedListNode<T> node, int index) =>
            {
                if (node.Value.Equals(key))
                {
                    output = node;
                }
            });

            return output;
        }
        public void ForEach(Action<LinkedListNode<T>, int> itterator)
        {
            if (first != null)
            {
                var currentNode = first;

                for (int index = 0; currentNode != null; index++)
                {
                    itterator(currentNode, index);
                    currentNode = currentNode.Next;
                }
            }
        }
        public void PushBack(T key)
        {
            var last = Last;

            if (last != null)
            {
                last.Next = new LinkedListNode<T>(key);
            }
            else
            {
                first = new LinkedListNode<T>(key);
            }
        }
        public void PushFront(T key)
        {
            var newFirst = new LinkedListNode<T>(key);

            if (first != null)
            {
                newFirst.Next = first;
                first = newFirst;
            }
            else
            {
                first = newFirst;
            }
        }
        public void AddAfter(LinkedListNode<T> node, T key)
        {
            var currentNext = node.Next;
            var newNext = new LinkedListNode<T>(key, currentNext);

            node.Next = newNext;
        }
        public void AddBefore(LinkedListNode<T> node, T key)
        {
            var copy = node.Clone();
            node.Value = key;
            node.Next = copy;
        }
        public void PushBackRange(T[] array)
        {
            foreach (var item in array)
            {
                PushBack(item);
            }
        }
        public void RemoveFirst()
        {
            if (first != null)
            {
                first = first.Next;
            }
        }
        public void RemoveLast()
        {
            LinkedListNode<T>? previousNode = null;

            ForEach((LinkedListNode<T> node, int index) =>
            {
                if (node.Next != null)
                {
                    previousNode = node;
                }
            });

            if (previousNode != null)
            {
                previousNode.Next = null;
            }
        }
        public void RemoveNode(LinkedListNode<T> node)
        {
            if (first != null)
            {
                var currentNode = first;
                LinkedListNode<T>? previousNode = null;

                for (; currentNode != null;)
                {
                    if (currentNode == node)
                    {
                        if (previousNode == null)
                        {
                            RemoveFirst();
                        }
                        else
                        {
                            previousNode.Next = currentNode.Next;
                        }
                        break;
                    }

                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }
            }
        }
        public void Remove(T key)
        {
            var node = Find(key);

            if (node != null)
            {
                RemoveNode(node);
            }
        }
        public void RemoveLast(T key)
        {
            var node = FindLast(key);

            if (node != null)
            {
                RemoveNode(node);
            }
        }
        public void RemoveAll(T key)
        {
            for (var node = FindLast(key); node != null; node = FindLast(key))
            {
                RemoveNode(node);
            }
        }
        public void Reverse()
        {
            LinkedListNode<T>? newFirst = null;

            ForEach((LinkedListNode<T> node, int index) =>
            {
                newFirst = new LinkedListNode<T>(node.Value, newFirst);
            });

            first = newFirst;
        }
        public void Clear()
        {
            first = null;
        }
    }
}
