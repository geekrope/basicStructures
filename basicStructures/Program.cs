using System.Text;

public class Node<T>
{
    public T Value
    {
        get; set;
    }
    public Node<T>? Next
    {
        get; set;
    }

    public Node<T> Clone()
    {
        return new Node<T>(Value, Next);
    }

    public Node(T value, Node<T>? next = null)
    {
        Value = value;
        Next = next;
    }
}

public class LinkedList<T>
{
    private Node<T>? first;

    private void ForEach(Action<Node<T>, int> itterator)
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
    private Node<T>? Last()
    {
        Node<T>? output = null;

        ForEach((Node<T> node, int index) =>
        {
            output = node;
        });

        return output;
    }

    public int GetCount()
    {
        int count = 0;

        ForEach((Node<T> node, int index) => { count++; });

        return count;
    }
    public string Print()
    {
        StringBuilder output = new();

        ForEach((Node<T> node, int index) => { output.Append(node.Value?.ToString() + " "); });

        return output.ToString();
    }
    public Node<T>? Find(T key)
    {
        Node<T>? output = null;

        ForEach((Node<T> node, int index) =>
        {
            if (output == null && node.Value.Equals(key))
            {
                output = node;
            }
        });

        return output;
    }
    public Node<T>? FindByIndex(int index)
    {
        Node<T>? output = null;

        ForEach((Node<T> node, int currentIndex) =>
        {
            if (output == null && currentIndex == index)
            {
                output = node;
            }
        });

        return output;
    }
    public Node<T>? FindLast(T key)
    {
        Node<T>? output = null;

        ForEach((Node<T> node, int index) =>
        {
            if (node.Value.Equals(key))
            {
                output = node;
            }
        });

        return output;
    }
    public void PushBack(T key)
    {
        var last = Last();

        if (last != null)
        {
            last.Next = new Node<T>(key);
        }
        else
        {
            first = new Node<T>(key);
        }
    }
    public void PushFront(T key)
    {
        var newFirst = new Node<T>(key);

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
    public void AddAfter(Node<T> node, T key)
    {
        var currentNext = node.Next;
        var newNext = new Node<T>(key, currentNext);

        node.Next = newNext;
    }
    public void AddBefore(Node<T> node, T key)
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
        Node<T>? previousNode = null;

        ForEach((Node<T> node, int index) =>
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
    public void RemoveNode(Node<T> node)
    {
        if (first != null)
        {
            var currentNode = first;
            Node<T>? previousNode = null;

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
        Node<T>? newFirst = null;

        ForEach((Node<T> node, int index) =>
        {
            newFirst = new Node<T>(node.Value, newFirst);
        });

        first = newFirst;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var list = new LinkedList<int>();

        list.PushBack(5);
        list.PushBack(1);
        list.PushBack(2);
        list.PushBack(3);
        list.PushBack(5);
        list.PushBack(4);
        list.PushBack(5);
        list.PushBack(5);
        list.PushBack(5);
        list.PushBack(5);
        list.PushBack(5);

        list.RemoveAll(1);

        Console.WriteLine(list.Print());
    }
}