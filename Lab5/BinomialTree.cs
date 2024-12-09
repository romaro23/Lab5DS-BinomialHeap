using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class BinomialNode<T>
    {
        public T Value { get; set; }
        public List<BinomialNode<T>> Children { get; set; }

        public BinomialNode(T value)
        {
            Value = value;
            Children = new List<BinomialNode<T>>();
        }
    }
    public class BinomialTree<T>
    {
        public BinomialNode<T> Root { get; private set; }
        public int Degree { get; set; }

        public BinomialTree(T rootValue)
        {
            Root = new BinomialNode<T>(rootValue);
            Degree = 0;
        }

        public void AddChild(BinomialTree<T> childTree)
        {
            if (childTree.Degree != Degree)
                throw new InvalidOperationException("Child tree degree must match current tree degree.");

            Root.Children.Add(childTree.Root);
            Degree++;
        }

        public int GetNodeCount()
        {
            return (int)Math.Pow(2, Degree);
        }

    }
    public class BinomialHeap<T> where T : IComparable<T>
    {
        private List<BinomialTree<T>> trees = new();

        public void Insert(T value)
        {
            var newTree = new BinomialTree<T>(value);
            MergeTree(newTree);
        }
        public T ExtractMin()
        {
            if (trees.Count == 0)
                throw new InvalidOperationException("Heap is empty.");

            int minIndex = 0;
            for (int i = 1; i < trees.Count; i++)
            {
                if (trees[i].Root.Value.CompareTo(trees[minIndex].Root.Value) < 0)
                {
                    minIndex = i;
                }
            }

            var minTree = trees[minIndex];
            trees.RemoveAt(minIndex);

            var childTrees = new List<BinomialTree<T>>();
            foreach (var child in minTree.Root.Children)
            {
                var childTree = new BinomialTree<T>(child.Value)
                {
                    Degree = child.Children.Count
                };
                childTree.Root.Children.AddRange(child.Children);
                childTrees.Add(childTree);
            }

            foreach (var childTree in childTrees)
            {
                MergeTree(childTree);
            }

            return minTree.Root.Value;
        }

        private void MergeTree(BinomialTree<T> newTree)
        {
            while (true)
            {
                var existingTree = trees.Find(tree => tree.Degree == newTree.Degree);
                if (existingTree == null)
                {
                    trees.Add(newTree);
                    trees.Sort((x, y) => x.Degree.CompareTo(y.Degree));
                    break;
                }
                trees.Remove(existingTree);
                if (Comparer<T>.Default.Compare(existingTree.Root.Value, newTree.Root.Value) < 0)
                {
                    existingTree.AddChild(newTree);
                    newTree = existingTree;
                }
                else
                {
                    newTree.AddChild(existingTree);
                }
            }
        }

        public int GetTotalNodeCount()
        {
            int count = 0;
            foreach (var tree in trees)
            {
                count += tree.GetNodeCount();
            }
            return count;
        }

        public string GetTreeCount()
        {
            int count = trees.Count;
            return $"Decimal: {count}, Binary: {Convert.ToString(count, 2)}";
        }

        public void PrintHeap()
        {
            foreach (var tree in trees)
            {
                Console.WriteLine($"Tree of degree {tree.Degree}");
                PrintTree(tree.Root, 0);
            }
        }

        private void PrintTree(BinomialNode<T> node, int indent)
        {
            Console.WriteLine($"{new string(' ', indent)}{node.Value}");
            foreach (var child in node.Children)
            {
                PrintTree(child, indent + 2);
            }
        }
    }
}