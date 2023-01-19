using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates a block on middle and the right
    /// </summary>
    public class LeftRightBlock : Terminal
    {
        public LeftRightBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.left, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.right, pathNumber);
            return new Grammar
            {
                NextPoint = start + Vector3.forward,
                Part = Random.value < 0.5 ? PathPart.LastLeftOne : PathPart.LastRightOne
            };
        }
    }
}