using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates a block in the middle and on the right
    /// </summary>
    public class RightMiddleBlock : Terminal
    {
        public RightMiddleBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.right, pathNumber);
            return new Grammar {NextPoint = start + Vector3.forward, Part = Random.value < 0.5 ? PathPart.LastRightOne : PathPart.LastMiddleOne};
        }
    }
}