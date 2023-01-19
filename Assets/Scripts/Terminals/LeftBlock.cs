using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    ///  Terminal which just creates a block on the left side
    /// </summary>
    public class LeftBlock : Terminal
    {
        public LeftBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + Vector3.left, pathNumber);
            return new Grammar
            {
                NextPoint = start + Vector3.forward,
                Part = PathPart.LastMiddleOne
            };
        }
    }
}