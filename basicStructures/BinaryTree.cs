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

        private int? root;

        public int Size
        {
            get
            {
                var branchSize1 = branch1 != null ? branch1.Size : 0;
                var branchSize2 = branch2 != null ? branch2.Size : 0;

                return branchSize1 + branchSize2 + (root != null ? 1 : 0);
            }
        }

        private BinaryTree Maximum()
        {
            if (branch2 == null)
            {
                return this;
            }
            else
            {
                return branch2.Maximum();
            }
        }
        private void Replace(BinaryTree with)
        {
            root = with.root;
            branch1 = with.branch1;
            branch2 = with.branch2;
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
        }
        public void Erase(int element)
        {
            var branches = Find(element);

            if (branches != null)
            {
                foreach (var branch in branches)
                {
                    if (branch.branch1 == null)
                    {
                        if (branch.branch2 != null)
                        {
                            branch.Replace(branch.branch2);
                        }
                        else
                        {
                            branch.root = null;
                        }
                    }
                    else
                    {
                        var max = branch.branch1.Maximum();
                        max.branch2 = branch.branch2;

                        branch.Replace(branch.branch1);
                    }
                }
            }
        }
        public BinaryTree[]? Find(int element)
        {
            var elementInFirstBranch = branch1?.Find(element);
            var elementInSecondBranch = branch2?.Find(element);

            elementInFirstBranch = elementInFirstBranch != null ? elementInFirstBranch : new BinaryTree[0];
            elementInSecondBranch = elementInSecondBranch != null ? elementInSecondBranch : new BinaryTree[0];

            var elementInBranches = elementInFirstBranch.Concat(elementInSecondBranch);

            if (root == element)
            {
                return new BinaryTree[] { this }.Concat(elementInBranches).ToArray();
            }
            else
            {
                return elementInBranches.ToArray();
            }
        }
        public string Print()
        {
            return branch1?.Print() + (root != null ? (" " + root) : "") + branch2?.Print();
        }

        public BinaryTree()
        {

        }
    }
}
