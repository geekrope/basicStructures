using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace basicStructures
{   
    class RegularBinarySearchTreeNode
    {
        private int _value;

        public int Value => _value;
        public RegularBinarySearchTreeNode? Left
        {
            get; set;
        }
        public RegularBinarySearchTreeNode? Right
        {
            get; set;
        }
        public IEnumerable<RegularBinarySearchTreeNode> Items
        {
            get
            {
                yield return this;
                var left = Left?.Items;
                var right = Right?.Items;

                if (left != null)
                {
                    foreach (var item in left)
                    {
                        yield return item;
                    }
                }
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

        public RegularBinarySearchTreeNode? EraseSelf()
        {
            RegularBinarySearchTreeNode? resultingNode;

            if (Left == null)
            {
                if (Right == null)
                {
                    resultingNode = null;
                }
                else
                {
                    resultingNode = Right;
                }
            }
            else
            {
                if (Right == null)
                {
                    resultingNode = Left;
                }
                else
                {
                    resultingNode = Right;

                    if (resultingNode.Left == null)
                    {
                        resultingNode.Left = Left;
                    }
                    else
                    {
                        RegularBinarySearchTreeNode? currentNode = resultingNode.Left;
                        RegularBinarySearchTreeNode previousNode = resultingNode.Left;

                        while (currentNode != null)
                        {
                            previousNode = currentNode;
                            currentNode = currentNode.Left;
                        }

                        previousNode.Left = Left;
                    }
                }
            }

            return resultingNode;
        }

        public RegularBinarySearchTreeNode(int value, RegularBinarySearchTreeNode? left = null, RegularBinarySearchTreeNode? right = null)
        {
            _value = value;
            Left = left;
            Right = right;
        }
    }

    class RegularBinarySearchTree : BinarySearchTree
    {
        private RegularBinarySearchTreeNode? root;

        public IEnumerable<RegularBinarySearchTreeNode>? Items
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

        private RegularBinarySearchTreeNode? BuildNode(int[] values, int left, int right)
        {
            if (left > right)
            {
                return null;
            }
            if (left == right)
            {
                return new RegularBinarySearchTreeNode(values[left]);
            }
            else
            {
                var mid = (left + right) / 2;
                var node = new RegularBinarySearchTreeNode(values[mid]);
                node.Left = BuildNode(values, left, mid - 1);
                node.Right = BuildNode(values, mid + 1, right);

                return node;
            }
        }
        private RegularBinarySearchTreeNode? FindParent(RegularBinarySearchTreeNode target, RegularBinarySearchTreeNode startingNode, RegularBinarySearchTreeNode? previous)
        {
            if (target == startingNode)
            {
                return previous;
            }

            RegularBinarySearchTreeNode? searchResult = null;

            if (startingNode.Left != null)
            {
                searchResult = FindParent(target, startingNode.Left, startingNode);

                if (searchResult != null)
                {
                    return searchResult;
                }
            }

            if (startingNode.Right != null)
            {
                searchResult = FindParent(target, startingNode.Right, startingNode);

                if (searchResult != null)
                {
                    return searchResult;
                }
            }

            return null;
        }
        private void Erase(RegularBinarySearchTreeNode node)
        {
            if (root == null)
            {
                throw new Exception("Tried to erase node in empty tree");
            }

            var parent = FindParent(node, root, null);

            if (parent == null)
            {
                root = node.EraseSelf();
            }
            else
            {
                if (parent.Left == node)
                {
                    parent.Left = node.EraseSelf();
                }
                else if (parent.Right == node)
                {
                    parent.Right = node.EraseSelf();
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

        public RegularBinarySearchTree(int[] array)
        {
            root = BuildNode(array, 0, array.Length - 1);
        }
    }
}
