using System;
using System.Text;

namespace basicStructures
{
    public class Queue<T>
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
        public void Enqueue(T element)
        {
            elements.PushFront(element);
        }
        public T? Dequeue()
        {
            var first = elements.First;

            if (first != null)
            {
                elements.RemoveFirst();
                return first.Value;
            }

            return default(T);
        }
        public T? Peek()
        {
            return elements.First != null ? elements.First.Value : default(T);
        }

        public Queue()
        {
            elements = new LinkedList<T>();
        }
    }
}
