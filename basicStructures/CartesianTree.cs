using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace basicStructures
{
    class CartesianTreeNode
    {
        private int _value;
        private int _priority;

        public int Value => _value;
        public int Priority => _priority;
        public int EntriesCount
        {
            get; set;
        }
        public CartesianTreeNode? Left
        {
            get; set;
        }
        public CartesianTreeNode? Right
        {
            get; set;
        }
        public IEnumerable<CartesianTreeNode> Items
        {
            get
            {
                var left = Left?.Items;
                var right = Right?.Items;

                if (left != null)
                {
                    foreach (var item in left)
                    {
                        yield return item;
                    }
                }
                yield return this;
                if (right != null)
                {
                    foreach (var item in right)
                    {
                        yield return item;
                    }
                }
            }
        }
        public int Height
        {
            get
            {
                if (Left == null)
                {
                    if (Right == null)
                    {
                        return 1;
                    }
                    else
                    {
                        return 1 + Right.Height;
                    }
                }
                else
                {
                    if (Right == null)
                    {
                        return 1 + Left.Height;
                    }
                    else
                    {
                        return Math.Max(Left.Height, Right.Height) + 1;
                    }
                }
            }
        }

        public CartesianTreeNode SubtleClone()
        {
            return new CartesianTreeNode(Value, Priority, Left, Right);
        }
        public CartesianTreeNode Clone()
        {
            return new CartesianTreeNode(Value, Priority, Left?.Clone(), Right?.Clone());
        }

        public CartesianTreeNode(int value, int priority, CartesianTreeNode? left = null, CartesianTreeNode? right = null)
        {
            _value = value;
            _priority = priority;
            Left = left;
            Right = right;
            EntriesCount = 1;
        }
    }

    class CartesianTree : BinarySearchTree
    {
        private CartesianTreeNode? root;

        public IEnumerable<CartesianTreeNode>? Items
        {
            get
            {
                return root?.Items;
            }
        }
        public int Height
        {
            get
            {
                if (root == null)
                {
                    return 0;
                }
                else
                {
                    return root.Height;
                }
            }
        }

        private (CartesianTreeNode?, CartesianTreeNode?) Split(CartesianTreeNode? node, int barrier)
        {
            if (node == null)
            {
                return (null, null);
            }
            else
            {
                var copy = node.SubtleClone();

                if (copy.Value <= barrier)
                {
                    var leftSide = copy;
                    var rightChildSplit = Split(copy.Right, barrier);

                    leftSide.Right = rightChildSplit.Item1;

                    return (leftSide, rightChildSplit.Item2);
                }
                else
                {
                    var rightSide = copy;
                    var leftChildSplit = Split(copy.Left, barrier);

                    rightSide.Left = leftChildSplit.Item2;

                    return (leftChildSplit.Item1, rightSide);
                }
            }
        }
        private CartesianTreeNode Merge(CartesianTreeNode left, CartesianTreeNode right)
        {
            CartesianTreeNode result;
            CartesianTreeNode leftClone = left.SubtleClone();
            CartesianTreeNode rightClone = right.SubtleClone();

            if (leftClone.Priority < rightClone.Priority)
            {
                result = leftClone;

                if (result.Right == null)
                {
                    result.Right = right;
                }
                else
                {
                    result.Right = Merge(result.Right, right);
                }
            }
            else
            {
                result = rightClone;

                if (result.Left == null)
                {
                    result.Left = left;
                }
                else
                {
                    result.Left = Merge(left, result.Left);
                }
            }

            return result;
        }
        private CartesianTreeNode? NullableMerge(CartesianTreeNode? left, CartesianTreeNode? right)
        {
            if (left == null)
            {
                if (right == null)
                {
                    return null;
                }
                else
                {
                    return right;
                }
            }
            else
            {
                if (right == null)
                {
                    return left;
                }
                else
                {
                    return Merge(left, right);
                }
            }
        }
        private CartesianTreeNode? Find(CartesianTreeNode root, int value)
        {
            if (root.Value == value)
            {
                return root;
            }
            else
            {
                CartesianTreeNode? result = null;

                if (root.Left != null)
                {
                    var leftScanResult = Find(root.Left, value);

                    result = leftScanResult != null ? leftScanResult : result;
                }
                if (root.Right != null)
                {
                    var rightScanResult = Find(root.Right, value);

                    result = rightScanResult != null ? rightScanResult : result;
                }

                return result;
            }
        }

        private void Erase(CartesianTreeNode node)
        {
            if (root == null)
            {
                return;
            }
            else
            {
                if (node.EntriesCount > 1)
                {
                    node.EntriesCount--;
                }
                else
                {
                    var rootSplit = Split(root, node.Value);
                    var leftSideSplit = Split(rootSplit.Item1, node.Value - 1);

                    root = NullableMerge(leftSideSplit.Item1, rootSplit.Item2);
                }
            }
        }

        public void Insert(int value)
        {
            var node = new CartesianTreeNode(value, RandomNumberGenerator.GetInt32(int.MaxValue));

            if (root == null)
            {
                root = node;
            }
            else
            {
                var valueEntry = Find(root, value);

                if (valueEntry == null)
                {
                    var rootSplit = Split(root, value);

                    if (value < root.Value)
                    {
                        rootSplit.Item1 = NullableMerge(rootSplit.Item1, node);
                        root = NullableMerge(rootSplit.Item1, rootSplit.Item2);
                    }
                    else
                    {
                        rootSplit.Item2 = NullableMerge(node, rootSplit.Item2);
                        root = NullableMerge(rootSplit.Item1, rootSplit.Item2);
                    }
                }
                else
                {
                    valueEntry.EntriesCount++;
                }
            }

        }
        public void Erase(int value)
        {
            if (root == null)
            {
                return;
            }
            else
            {
                var valueEntry = Find(root, value);

                if (valueEntry != null)
                {
                    if (valueEntry.EntriesCount > 1)
                    {
                        valueEntry.EntriesCount--;
                    }
                    else
                    {
                        var rootSplit = Split(root, value);
                        var leftSideSplit = Split(rootSplit.Item1, value - 1);

                        root = NullableMerge(leftSideSplit.Item1, rootSplit.Item2);
                    }
                }
            }
        }

        public int FindNearest(int target)
        {
            var currentNode = root;

            while (currentNode != null)
            {
                if (currentNode.Value == target)
                {
                    Erase(currentNode);
                    return currentNode.Value;
                }

                if (currentNode.Value > target)
                {
                    if (currentNode.Left == null || currentNode.Left.Value < target)
                    {
                        Erase(currentNode);
                        return currentNode.Value;
                    }
                    currentNode = currentNode.Left;
                }
                else
                {
                    if (currentNode.Right == null)
                    {
                        Erase(currentNode);
                        return currentNode.Value;
                    }

                    currentNode = currentNode.Right;
                }
            }

            return -1;
        }

        public CartesianTree(int[] array)
        {
            foreach (var item in array)
            {
                Insert(item);
            }
        }
    }
}
