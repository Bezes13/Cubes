using Model;
using NonTerminals;
using Path;
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
            PathModel.CreateObject(PathModel.PrefabType.Cube, start + Vector3.left, pathNumber);
            PathModel.CreateObject(PathModel.PrefabType.Cube, start, pathNumber);
            return new Grammar()
            {
                NextPoint = start + Vector3.forward,
                Part = Random.value < 0.5 ? PathPart.LastLeftOne : PathPart.LastMiddleOne
            };
        }
    }
}