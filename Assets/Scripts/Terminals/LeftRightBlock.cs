using Model;
using NonTerminals;
using UnityEngine;

namespace Terminals
{
    public class LeftRightBlock : Terminal
    {
        public LeftRightBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.left, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            return new Grammar() {NextPoint = start + Vector3.forward, Part = PathPart.Chaos};
        }
    }
}