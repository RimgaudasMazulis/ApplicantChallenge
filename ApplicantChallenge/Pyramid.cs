using ApplicantChallenge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicantChallenge
{
    public class Pyramid : IPyramid
    {
        private string _filePath { get; set; }
        private const char Separator = ' ';

        // Used for saving previous line values to assign as children nodes for parent nodes
        private List<Node> _previousLineNodes;

        // Used to save current nodes and then assign to other lists
        private List<Node> _currentLineNodes;

        // Contains all the numbers in the pyramid
        private List<Node> _nodes;

        // Used to save all possible (valid) paths
        private IEnumerable<List<int>> _validPaths;

        // Top pyramid node contains all children nodes in itself
        public Node PyramidTopNode { get; private set; }

        public IEnumerable<Node> Nodes
        {
            get { return _nodes; }
        }

        public IEnumerable<IEnumerable<int>> ValidPaths
        {
            get { return _validPaths; }
        }

        public Pyramid(String filePath)
        {
            _nodes = new List<Node>();
            _previousLineNodes = new List<Node>();
            _currentLineNodes = new List<Node>();
            _filePath = filePath;

            BuildNodePyramid();
        }
        
        /// <summary>
        ///     This method builds a pyramid that is made of nodes filled with given numbers
        /// </summary>
        public void BuildNodePyramid()
        {
            var lines = FileReader.ReadFile(_filePath).ToArray();
            if (!lines.Any())
            {
                Console.WriteLine("Input file ({0}) did not contain any data.", _filePath);
            }

            // Reverse list to work from bottom rows up
            Array.Reverse(lines);

            for (int l = 0; l < lines.Count(); l++)
            {
                var numbers = lines[l].Split(Separator);

                for (int n = 0; n < numbers.Count(); n++)
                {
                    AddNode(numbers, n);
                }

                _previousLineNodes.Clear();
                _previousLineNodes.AddRange(_currentLineNodes);
                _nodes.AddRange(_currentLineNodes);
                _currentLineNodes.Clear();
            }

            PyramidTopNode = _previousLineNodes.FirstOrDefault();
        }

        /// <summary>
        ///     This method prints maximum SUM of valid path in a console window
        /// </summary>
        public void PrintMaxSum()
        {
            _validPaths = GetValidPaths(PyramidTopNode);

            var maxSumSequence = _validPaths.OrderByDescending(x => x.Sum(i => i)).First();
            var maxSum = maxSumSequence.Sum(item => item);

            Console.WriteLine("Max sum: {0}\r\nPath: {1}\r\n", maxSum, String.Join(",", maxSumSequence));

            //All valid paths:
            Console.WriteLine("All possible valid paths:\n");
            foreach (var path in _validPaths)
            {
                Console.WriteLine("Sum: {0}\r\nPath: {1}\r\n", path.Sum(item => item), String.Join(",", path));
            }

            Console.ReadLine();
        }

        /// <summary>
        ///     This method returns valid paths from given top pyramid node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private IEnumerable<List<int>> GetValidPaths(Node node)
        {
            List<List<int>> validPaths = new List<List<int>>();
            Stack<Node> nodeStack = new Stack<Node>();
            List<Node> visited = new List<Node>();

            nodeStack.Push(node);

            while (nodeStack.Any())
            {
                node = nodeStack.Pop();

                if (visited.Contains(node))
                    continue;

                visited.Add(node);

                foreach (Node n in node.Children)
                {
                    if (IsChildrenNumberTypeDifferent(node.Value, n.Value)
                        && !visited.Contains(n))
                    {
                        n.BackTrackingParent = node; // To record a correct parent. One node can have multiple parents (in this case 2)
                        nodeStack.Push(n);
                    }
                }

                if (!node.Children.Any())
                {
                    var validSequence = GetBacktrackedNodeSequence(node).ToList();
                    validPaths.Add(validSequence);
                }
            }

            return validPaths;
        }      
        
        /// <summary>
        ///     This method backtacks valid path to the first element from the last node in the sequence.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private IEnumerable<int> GetBacktrackedNodeSequence(Node node)
        {
            List<int> sequence = new List<int>();
            while(node != null)
            {
                sequence.Add(node.Value);
                node = node.BackTrackingParent;
            }
            sequence.Reverse();

            return sequence;
        }

        /// <summary>
        ///     This method determines if the child node is subsequently different from the parent
        /// </summary>
        /// <param name="parentNumber"></param>
        /// <param name="childNumber"></param>
        /// <returns></returns>
        private bool IsChildrenNumberTypeDifferent(int parentNumber, int childNumber)
        {
            if(parentNumber % 2 != childNumber % 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        
        /// <summary>
        ///     This method created and also adds a node to the a temporary _previousLineNodes list 
        /// </summary>
        /// 
        /// <param name="numbers"></param>
        ///     This parameter represents the numbers in a line
        /// <param name="indexOfCurrentNumber"></param>
        ///     This aprameter represents index of the current number in the line
        ///     
        private void AddNode(String[] numbers, int indexOfCurrentNumber)
        {
            if (int.TryParse(numbers[indexOfCurrentNumber], out int nodeNumber))
            {
                Node node = new Node(nodeNumber);

                if (_previousLineNodes.Any())
                {
                    // Because this is a pyramid, every lower level has one more element in the row than previois row, thus for each upper level index x, its children will be x and x + 1
                    for (int i = indexOfCurrentNumber; i < indexOfCurrentNumber + 2; i++) 
                    {
                        _previousLineNodes[i].AddParent(node);
                        node.AddChildren(_previousLineNodes[i]);
                    }
                }

                _currentLineNodes.Add(node);
            }
            else
            {
                Console.WriteLine("Error in file ({0}) ! ({1}) is not a number.", _filePath, numbers[indexOfCurrentNumber]);
            }
        }
    }
}
