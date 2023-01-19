using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates a triple block one in the middle, one on the left and one on the right.
    /// </summary>
    public class TripleBlock : Terminal
    {
        public TripleBlock(PathModel pathModel) : base(pathModel)
        {
        }
        
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + new Vector3(1, 0, 0), pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + new Vector3(-1, 0, 0), pathNumber);
            return new Grammar {NextPoint = start + Vector3.forward, Part = PathPart.Chaos};
        }
    }
}