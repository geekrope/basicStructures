using System;
using System.Text;

namespace basicStructures
{
    public class Stack<T>
    {
        private LinkedList<T> elements;

        private void ForEach(Action<T> itterator)
        {
            elements.ForEach((node, index) => { itterator(node.Value); });
        }

        public string Print()
        {
            var output = new StringBuilder();

            ForEach((element) => { output.Append(element.ToString() + " "); });

            return output.ToString();
        }
        public bool Empty()
        {
            return elements.Count == 0;
        }
        public void Push(T key)
        {
            elements.PushFront(key);
        }
        public void Pop()
        {
            elements.RemoveFirst();
        }
        public T? Top()
        {
            if (elements.First != null)
            {
                return elements.First.Value;
            }
            else
            {
                return default(T);
            }
        }
        public void Clear()
        {
            elements.Clear();
        }

        public Stack()
        {
            elements = new LinkedList<T>();
        }
    }
}
