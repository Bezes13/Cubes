using Model;
using UnityEngine;

namespace Terminals
{
    public class LeftMiddleBlock : Terminal
    {
        public LeftMiddleBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.left, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            return new Grammar() {NextPoint = start + Vector3.forward};
        }
    }
}