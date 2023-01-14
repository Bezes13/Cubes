using Model;
using UnityEngine;

namespace NonTerminals
{
    public class PathSplitter : NonTerminal
    {

        public PathSplitter(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber, true);
            int newPath = PathModel.CreateNewPath(start + Vector3.forward * 4 + Vector3.left * 4, pathNumber);
            PathModel.LeftSweep.Create(start + Vector3.forward, newPath);
            PathModel.SingleBlock.Create(start + Vector3.forward*2 + Vector3.left*2, newPath);
            PathModel.LeftSweep.Create(start + Vector3.forward*3 + Vector3.left*2, newPath);
            PathModel.GetGeneratorByNumber(pathNumber).type = PathModel.Prefabtype.Cube;
            PathModel.RightSweep.Create(start + Vector3.forward, pathNumber);
            PathModel.SingleBlock.Create(start + Vector3.forward*2 + Vector3.right*2, pathNumber);
            return new Grammar {Part = PathPart.RightSweep, NextPoint = start + Vector3.forward*3 + Vector3.right*2};
        }
    }
}