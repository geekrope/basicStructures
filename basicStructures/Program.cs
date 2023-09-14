using System;

namespace basicStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tree = new BinaryTree();

            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(8);
            tree.Insert(0);
            tree.Insert(-3);
            tree.Insert(-4);

            tree.Erase(8);

            Console.WriteLine(tree.Print());
        }
    }
}