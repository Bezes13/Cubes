using UnityEngine;

namespace NonTerminals
{
    public class HoleOrBlock : NonTerminal
    {
        public HoleOrBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            return Random.value > 0.9 ? PathModel.Hole.Create(start, pathNumber) : PathModel.RandomTripletAtLeastOne.Create(start, pathNumber);
        }
    }
}