using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    public class SingleBlock : Terminal
    {
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.PrefabType.Cube, start, pathNumber);
            return new Grammar() {NextPoint = start + Vector3.forward, Part = PathPart.LastMiddleOne};
        }

        public SingleBlock(PathModel pathModel) : base(pathModel)
        {
            
        }
    }
}