using Model;
using NonTerminals;
using UnityEngine;

namespace Terminals
{
    public class UpStairs : Terminal
    {
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(0, 1, 1), pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(0, 1, 2), pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(0, 2, 3), pathNumber);
            return new Grammar() {NextPoint = start + new Vector3(0, 2, 4), Part = PathPart.AfterStairs};
        }

        public UpStairs(PathModel pathModel) : base(pathModel)
        {
        }
    }
}