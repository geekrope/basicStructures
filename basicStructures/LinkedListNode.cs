using System;

namespace basicStructures
{
    public class LinkedListNode<T>
    {
        public T Value
        {
            get; set;
        }
        public LinkedListNode<T>? Next
        {
            get; set;
        }

        public LinkedListNode<T> Clone()
        {
            return new LinkedListNode<T>(Value, Next);
        }

        public LinkedListNode(T value, LinkedListNode<T>? next = null)
        {
            Value = value;
            Next = next;
        }
    }
}
