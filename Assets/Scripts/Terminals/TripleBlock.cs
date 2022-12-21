using DefaultNamespace;
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
        
        public override Vector3 Create(Vector3 start)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(1, 0, 0));
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(-1, 0, 0));
            return start + Vector3.forward;
        }

        private void RandomTile(Vector3 pos)
        {
            var rnd = _rnd.NextDouble();
            if (rnd <= 0.1)
            {
                return;
            }

            PathModel.CreateObject(PathModel.Prefabtype.Cube, pos);
            if (rnd >= 0.9)
            {
                PathModel.CreateObject(PathModel.Prefabtype.Cube, pos + Vector3.up);
            }
        }
    }
}