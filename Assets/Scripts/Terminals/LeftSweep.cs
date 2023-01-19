using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which moves the mainPath three Blocks to the side.
    /// </summary>
    public class LeftSweep : Terminal
    {
        public LeftSweep(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.left, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.left * 2, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.left * 3, pathNumber);
            return new Grammar {NextPoint = start + Vector3.left * 3 + Vector3.forward, Part = PathPart.AfterSweep};
        }
    }
}