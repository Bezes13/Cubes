using UnityEngine;
namespace Terminals
{
    public class RandomTripletAtLeastOne : Terminal
    {
        private float _difficulty;

        public RandomTripletAtLeastOne(PathModel pathModel, float difficulty) : base(pathModel)
        {
            _difficulty = difficulty;
        }

        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            var onePlaced = RandomTile(start, pathNumber);
            onePlaced = RandomTile(start + Vector3.right, pathNumber) || onePlaced;
            onePlaced = RandomTile(start + Vector3.left, pathNumber) || onePlaced;

            if (!onePlaced)
            {
                float rnd = Random.value;
                Vector3 pos = rnd > 0.66 ? Vector3.zero : rnd < 0.33 ? Vector3.right : Vector3.left;
                PathModel.CreateObject(PathModel.Prefabtype.Cube, start + pos, pathNumber);
            }
            return start + Vector3.forward;
        }
        
        private bool RandomTile(Vector3 pos, int pathNumber)
        {
            
            if (Random.value <= 0.3 + _difficulty)
            {
                return false;
            }
          
            PathModel.CreateObject(PathModel.Prefabtype.Cube, pos, pathNumber);

            return true;
        }
        
        public void Increase(float difficulty)
        {
            _difficulty = difficulty;
        }
    }
}