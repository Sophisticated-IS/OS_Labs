using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
 
namespace TreeTestConsoleApplication
{
    class BinaryTree<T>
    {
        /// <summary>
        /// The tree is build up out of BinaryTreeNode instances
        /// </summary>
 
        class BinaryTreeNode
        {
            public BinaryTreeNode Left;
            public BinaryTreeNode Right;
            public BinaryTreeNode Parent;
            public T Data;
 
            public BinaryTreeNode()
            {
                Left = null;
                Right = null;
                Parent = null;
            }
        }
 
        BinaryTreeNode Root;
        Comparison<T> CompareFunction;
 
        /// <summary>
        /// The BinaryTree constructor requires that we pass a comparison function. We need one as generics can only
        /// be compared as equals, but not for order. The solution is to allow the caller to pass a suitable comparison
        /// function. We use the C# Comparison delegate for this (found in System.Collections)
        /// </summary>
        /// <param name="theCompareFunction">Pass a delegate function of the type Comparison to the function</param>
 
        public BinaryTree(Comparison<T> theCompareFunction)
        {
            Root = null;
            CompareFunction = theCompareFunction;
        }
 
        /// <summary>
        /// For integer comparisons we provide a demonstration function.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>&lt;0 for left smaller than right, &gt;0 if they are equal, +1 if right is larger than left</returns>
        public static int CompareFunction_Int(int left, int right)
        {
            return left - right;
        }
 
        /// <summary>
        /// For string comparisons we provide a demonstration function
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>-1 for left smaller than right, 0 if they are equal, +1 if right is larger than left</returns>
        public static int CompareFunction_String(string left, string right)
        {
            return left.CompareTo(right);
        }
 
        /// <summary>
        /// The add function uses non-recursive tree traversal to find the next available insertion point
        /// </summary>
        /// <param name="Value">The value to insert into tree.</param>
        public void Add(T Value)
        {
            BinaryTreeNode child = new BinaryTreeNode();
            child.Data = Value;
 
            // Is the tree empty? Make the root the new child
            if (Root == null)
            {
                Root = child;
            }
            else
            {
                // Start from the root of the tree
                BinaryTreeNode Iterator = Root;
                while (true)
                {
                    // Compare the value to insert with the value in the current tree node
                    int Compare = CompareFunction(Value, Iterator.Data);
                    // The value is smaller or equal to the current node, we need to store it on the left side
                    // We test for equivalence as we allow duplicates (!)
                    if (Compare <= 0)
                        if (Iterator.Left != null)
                        {
                            // Travel further left
                            Iterator = Iterator.Left;
                            continue;
                        }
                        else
                        {
                            // An empty left leg, add the new node on the left leg
                            Iterator.Left = child;
                            child.Parent = Iterator;
                            break;
                        }
                    if (Compare > 0)
                        if (Iterator.Right != null)
                        {
                            // Continue to travel right
                            Iterator = Iterator.Right;
                            continue;
                        }
                        else
                        {
                            // Add the child to the right leg
                            Iterator.Right = child;
                            child.Parent = Iterator;
                            break;
                        }
                }
 
            }
 
        }
 
        /// <summary>
        /// This routine walks through the tree to see if the value given can be found.
        /// </summary>
        /// <param name="Value">The value to look for in the tree</param>
        /// <returns>True if found, False if not found</returns>
        public bool Find(T Value)
        {
            BinaryTreeNode Iterator = Root;
            while (Iterator != null)
            {
                int Compare = CompareFunction(Value, Iterator.Data);
                // Did we find the value ?
                if (Compare == 0) return true;
                if (Compare < 0)
                {
                    // Travel left
                    Iterator = Iterator.Left;
                    continue;
                }
                // Travel right
                Iterator = Iterator.Right;
            }
            return false;
        }
 
        /// <summary>
        /// Given a starting node, this routine will locate the left most node in the sub-tree
        /// If no further nodes are found, it returns the starting node
        /// </summary>
        /// <param name="start">The sub-tree starting point</param>
        /// <returns></returns>
        BinaryTreeNode FindMostLeft(BinaryTreeNode start)
        {
            BinaryTreeNode node = start;
            while (true)
            {
                if (node.Left != null)
                {
                    node = node.Left;
                    continue;
                }
                break;
            }
            return node;
        }
 
        /// <summary>
        /// Returns a list iterator of the elements in the tree implementing the IENumerator interface.
        /// </summary>
        /// <returns>IENumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new BinaryTreeEnumerator(this);
        }
 
        /// <summary>
        /// The BinaryTreeEnumerator implements the IEnumerator allowing foreach enumeration of the tree
        /// </summary>
        class BinaryTreeEnumerator : IEnumerator<T>
        {
            BinaryTreeNode current;
            BinaryTree<T> theTree;
 
            public BinaryTreeEnumerator(BinaryTree<T> tree)
            {
                theTree = tree;
                current = null;
            }
 
            /// <summary>
            /// The MoveNext function traverses the tree in sorted order.
            /// </summary>
            /// <returns>True if we found a valid entry, False if we have reached the end</returns>
            public bool MoveNext()
            {
                // For the first entry, find the lowest valued node in the tree
                if (current == null)
                    current = theTree.FindMostLeft(theTree.Root);
                else
                {
                    // Can we go right-left?
                    if (current.Right != null)
                        current = theTree.FindMostLeft(current.Right);
                    else
                    {
                        // Note the value we have found
                        T CurrentValue = current.Data;
 
                        // Go up the tree until we find a value larger than the largest we have
                        // already found (or if we reach the root of the tree)
                        while (current != null)
                        {
                            current = current.Parent;
                            if (current != null)
                            {
                                int Compare = theTree.CompareFunction(current.Data, CurrentValue);
                                if (Compare < 0) continue;
                            }
                            break;
                        }
 
                    }
                }
                return (current != null);
            }
 
            public T Current
            {
                get
                {
                    if (current == null)
                        throw new InvalidOperationException();
                    return current.Data;
                }
            }
 
            object IEnumerator.Current
            {
                get
                {
                    if (current == null)
                        throw new InvalidOperationException();
                    return current.Data;
                }
            }
 
            public void Dispose() { }
            public void Reset() { current = null; }
        }
    }
}