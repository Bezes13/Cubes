using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    public class RightMiddleBlock : Terminal
    {
        public RightMiddleBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.right, pathNumber);
            return new Grammar() {NextPoint = start + Vector3.forward, Part = PathPart.Chaos};
        }
    }
}