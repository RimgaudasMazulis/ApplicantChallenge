using System.Collections.Generic;

namespace ApplicantChallenge
{
    public class Node
    {
        private List<Node> _children;
        private List<Node> _parents; 

        public int Value { get; set; }
        public Node BackTrackingParent { get; set; }

        public IEnumerable<Node> Children
        {
            get
            {
                return _children;
            }
        }

        public IEnumerable<Node> Parents
        {
            get
            {
                return _parents;
            }
        }

        public Node(int value)
        {
            Value = value;
            _children = new List<Node>();
            _parents = new List<Node>();

        }

        public void AddChildren(Node children)
        {
            _children.Add(children);
        }

        public void AddParent(Node parent)
        {
            _parents.Add(parent);
        }
    }
}
