using Model;
using UnityEngine;

namespace NonTerminals
{
    public class PathSplitter : NonTerminal
    {
        private readonly PathGenerator _pathGenerator;

        public PathSplitter(PathModel pathModel, PathGenerator pathGenerator) : base(pathModel)
        {
            _pathGenerator = pathGenerator;
        }

        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            PathModel.SingleBlock.Create(start, pathNumber);
            PathModel.CreateNewPath(PathModel.LeftSweep.Create(start + Vector3.forward, pathNumber), pathNumber);
            return PathModel.RightSweep.Create(start + Vector3.forward, pathNumber);
        }
    }
}