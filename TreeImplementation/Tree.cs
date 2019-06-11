using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeImplementation
{
    public class Tree<T> where T : class
    {
        public T Value { get; private set; }
        public Tree<T> Parent { get; set; }

        public Tree<T> Left { get; set; }
        public Tree<T> Right { get; set; }

        public List<Tree<T>> Children { get; private set; }

        

        public Tree()
        {
            Children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children) : this()
        {
            this.Value = value;

            this.Children = AddChildren(children);

        }

        public Tree(T value, Tree<T> left, Tree<T> right) : this()
        {
            Value = value;
            Left = left;
            Right = right;
        }

        public List<Tree<T>> AddChildren(params Tree<T>[] children)
        {
            foreach (var child in children)
            {
                this.Children.Add(child);
                child.Parent = this;
            }

            return Children;
        }

        public void AddChild(Tree<T> item)
        {
            Children.Add(item);
        }

        public void RemoveChild(Tree<T> item)
        {
            Children.Remove(item);
        }

        public void PrintTree(int indent = 0)
        {
            Console.Write(String.Concat(Enumerable.Repeat("├──", indent)));
            Console.WriteLine(this.Value);

            indent++;
            foreach (var child in this.Children)
            {
                child.PrintTree(indent);
            }
        }


        public bool Contains(Tree<T> item)
        {
            if (this.Value == item.Value)
            {
                return true;
            }

            foreach (var child in this.Children)
            {
                if( child.Value == item.Value)
                {
                    return true;
                }
                Contains(child);
            }

            return false;
        }

        public bool Contains(Func<Tree<T>, bool> predicate, Tree<T> arg)
        {
            return predicate(arg);
        }

        public void TraverseBreadthFirst(Tree<T> root, List<T> list)
        {
            if (root == null) return;

            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                list.Add(node.Value);

                if (node.Left != null)
                    queue.Enqueue(node.Left);

                if (node.Right != null)
                    queue.Enqueue(node.Right);

            }
        }

    }
}
