using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    public class TripleBlock : Terminal
    {
        public TripleBlock(PathModel pathModel) : base(pathModel)
        {
        }
        
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(1, 0, 0), pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(-1, 0, 0), pathNumber);
            return new Grammar() {NextPoint = start + Vector3.forward, Part = PathPart.Chaos};
        }
    }
}