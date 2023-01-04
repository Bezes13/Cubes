using Model;
using UnityEngine;

namespace NonTerminals
{
    public class BlockPart : NonTerminal
    {
        public BlockPart(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            Vector3 next = PathModel.RandomTripletAtLeastOne.Create(start, pathNumber);
            next = PathModel.HoleOrBlock.Create(next, pathNumber);
            next = PathModel.HoleOrBlock.Create(next, pathNumber);
            next = PathModel.RandomTripletAtLeastOne.Create(next, pathNumber);
            return next;
        }
    }
}