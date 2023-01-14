using DefaultNamespace;
using Model;
using NonTerminals;
using UnityEngine;
using Random = System.Random;

namespace Terminals
{
    public class TripleBlock : Terminal
    {
        private readonly Random _rnd;
        public TripleBlock(PathModel pathModel) : base(pathModel)
        {
            _rnd = new Random();
        }
        
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(1, 0, 0), pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(-1, 0, 0), pathNumber);
            return new Grammar() {NextPoint = start + Vector3.forward, Part = PathPart.Chaos};
        }

        private void RandomTile(Vector3 pos, int pathNumber)
        {
            var rnd = _rnd.NextDouble();
            if (rnd <= 0.1)
            {
                return;
            }

            PathModel.CreateObject(PathModel.Prefabtype.Cube, pos, pathNumber);
            if (rnd >= 0.9)
            {
                PathModel.CreateObject(PathModel.Prefabtype.Cube, pos + Vector3.up, pathNumber);
            }
        }
    }
}