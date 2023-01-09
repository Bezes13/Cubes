using DefaultNamespace;
using Model;
using NonTerminals;
using UnityEngine;

namespace Terminals
{
    public class RightSweep : Terminal
    {
        public RightSweep(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.right, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.right * 2, pathNumber);
            return new Grammar()
            {
                NextPoint = start + Vector3.right * 2 + Vector3.forward,
                Part = PathPart.AfterSweep
            };
        }
    }
}