using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates a Log on the right side and places triple blocks under the log with at least one on the left
    /// </summary>
    public class RightSweep : Terminal
    {
        public RightSweep(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.right, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.right * 2, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.right * 3, pathNumber);
            return new Grammar
            {
                NextPoint = start + Vector3.right * 3 + Vector3.forward,
                Part = PathPart.AfterSweep
            };
        }
    }
}