using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    public class RightBlock : Terminal
    {
        public RightBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.PrefabType.Cube, start + Vector3.right, pathNumber);
            return new Grammar {NextPoint = start + Vector3.forward, Part = PathPart.LastRightOne};
        }
    }
}