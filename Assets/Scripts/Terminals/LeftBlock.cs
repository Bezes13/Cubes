using Model;
using UnityEngine;

namespace Terminals
{
    public class LeftBlock : Terminal
    {
        public LeftBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.left, pathNumber);
            return new Grammar() {NextPoint = start + Vector3.forward};
        }
    }
}