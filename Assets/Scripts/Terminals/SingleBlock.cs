using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates one block in the middle
    /// </summary>
    public class SingleBlock : Terminal
    {
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start, pathNumber);
            return new Grammar {NextPoint = start + Vector3.forward, Part = PathPart.LastMiddleOne};
        }

        public SingleBlock(PathModel pathModel) : base(pathModel)
        {
        }
    }
}