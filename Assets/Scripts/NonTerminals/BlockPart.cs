using UnityEngine;

namespace NonTerminals
{
    public class BlockPart : NonTerminal
    {
        public BlockPart(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start)
        {
            Vector3 next = PathModel.RandomTripletAtLeastOne.Create(start);
            next = PathModel.HoleOrBlock.Create(next);
            next = PathModel.HoleOrBlock.Create(next);
            next = PathModel.RandomTripletAtLeastOne.Create(next);
            return next;
        }
    }
}