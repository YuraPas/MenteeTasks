using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<string> firstNode = new Tree<string>("less");
            Tree<string> secondNode = new Tree<string>("dist", left: new Tree<string>("Program.cs"), right: new Tree<string>("Tree.cs"));
            Tree<string> thirdNode = new Tree<string>("css", new Tree<string>("bootstrap.css"), new Tree<string>("bootstrap.min.css"));
            Tree<string> fourthNode = new Tree<string>("docs");

            Tree<string> tree = new Tree<string>("bootstrap", firstNode, secondNode, thirdNode,fourthNode);
            tree.PrintTree();

            #region Output

            /* 
                bootstrap
                ├──less
                ├──dist
                ├──css
                ├──├──bootstrap.css
                ├──├──bootstrap.min.css
                ├──docs
             */
            #endregion

            tree.RemoveChild(secondNode);
            tree.PrintTree();

            #region Output

            /* 
               bootstrap
               ├──less
               ├──css
               ├──├──bootstrap.css
               ├──├──bootstrap.min.css
               ├──docs
            */
            #endregion

            //винести в тести 
            Tree<string> testNode = new Tree<string>("less");

            Tree<string> falseTestNode = new Tree<string>("ldfdess");

            Console.WriteLine(tree.Contains(testNode)); //True
            Console.WriteLine(tree.Contains(falseTestNode)); //False

            Func<Tree<string>, bool> predicate = tree.Contains;
           
            Console.WriteLine(tree.Contains(predicate,testNode));

            List<string> result = new List<string>();
            Tree<string> testObj = new Tree<string>("bootstrap", left : secondNode, right : thirdNode);
            tree.TraverseBreadthFirst(testObj, result);
            Console.WriteLine(result);// в тести стаб винести
            Console.ReadKey();
        }
    }
}
