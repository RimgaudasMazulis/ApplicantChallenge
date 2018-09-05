using System.Collections.Generic;

namespace ApplicantChallenge.Interfaces
{
    public interface IPyramid
    {
        Node PyramidTopNode { get; }
        IEnumerable<Node> Nodes { get; }
        IEnumerable<IEnumerable<int>> ValidPaths { get; }

        void BuildNodePyramid();
        void PrintMaxSum();
    }
}
