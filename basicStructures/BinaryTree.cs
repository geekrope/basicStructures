using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicStructures
{
    class BinaryTree
    {
        private BinaryTree? branch1;
        private BinaryTree? branch2;
        private int size;

        private int? root;

        public int Size
        {
            get => size;
        }

        public void Insert(int element)
        {
            if (root == null)
            {
                root = element;
            }
            else
            {
                BinaryTree insertBranch;

                if (element < root)
                {
                    if (branch1 == null)
                    {
                        branch1 = new BinaryTree();
                    }

                    insertBranch = branch1;
                }
                else
                {
                    if (branch2 == null)
                    {
                        branch2 = new BinaryTree();
                    }

                    insertBranch = branch2;
                }

                insertBranch.Insert(element);
            }

            size++;
        }
        public BinaryTree[]? Find(int element)
        {
            var elementInFirstBranch = branch1?.Find(element);
            var elementInSecondBranch = branch2?.Find(element);
            var elementInBranches = elementInFirstBranch?.Concat(elementInSecondBranch != null ? elementInSecondBranch : new BinaryTree[0]);

            if (root == element)
            {
                return new BinaryTree[] { this }.Concat(elementInBranches != null ? elementInBranches : new BinaryTree[0]).ToArray();
            }
            else
            {
                return elementInBranches?.ToArray();
            }
        }
        public string Print()
        {
            return branch1?.Print() + " " + root + branch2?.Print();
        }

        public BinaryTree()
        {

        }
    }
}
