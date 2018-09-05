using System.Collections.Generic;

namespace ApplicantChallenge
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
