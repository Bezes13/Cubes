using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal Part which represents a path split, where a new Path gets created
    /// </summary>
    public class PathSplitter : Terminal
    {
        public PathSplitter(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            // New path
            var newPath = PathModel.CreateNewPath(start + Vector3.forward * 4 + Vector3.left * 6, pathNumber);
            PathModel.GetGeneratorByNumber(pathNumber).type = PathModel.PrefabType.Cube;

            // first block
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start, pathNumber, true);
            
            // first sweep
            PathModel.LeftSweep.Create(start + Vector3.forward, newPath);
            PathModel.RightSweep.Create(start + Vector3.forward, pathNumber);

            // SingleBlock
            PathModel.SingleBlock.Create(start + Vector3.forward * 2 + Vector3.left * 3, newPath);
            PathModel.SingleBlock.Create(start + Vector3.forward * 2 + Vector3.right * 3, pathNumber);

            // Last Sweep
            PathModel.LeftSweep.Create(start + Vector3.forward * 3 + Vector3.left * 3, newPath);
            return new Grammar
            {
                Part = PathPart.RightSweep,
                NextPoint = start + Vector3.forward * 3 + Vector3.right * 3
            };
        }
    }
}