using UnityEngine;
using Random = System.Random;
namespace Terminals
{
    public class RandomTripletAtLeastOne : Terminal
    {
        private readonly Random _rnd;
        public RandomTripletAtLeastOne(PathModel pathModel) : base(pathModel)
        {
            _rnd = new Random();
        }

        public override Vector3 Create(Vector3 start)
        {
            var onePlaced = RandomTile(start);
            onePlaced = onePlaced || RandomTile(start + Vector3.right);
            onePlaced = onePlaced || RandomTile(start + Vector3.left);

            if (!onePlaced)
            {
                PathModel.CreateObject(PathModel.Prefabtype.Cube, start);

            }
            return start + Vector3.forward;
        }
        
        private bool RandomTile(Vector3 pos)
        {
            var rnd = _rnd.NextDouble();
            if (rnd <= 0.2)
            {
                return false;
            }

            PathModel.CreateObject(PathModel.Prefabtype.Cube, pos);

            return true;
        }
    }
}