using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    public class BlockPart : NonTerminal
    {
        public BlockPart(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            Vector3 next = PathModel.RandomTripletAtLeastOne.Create(start, pathNumber).NextPoint;
            next = PathModel.HoleOrBlock.Create(next, pathNumber).NextPoint;
            next = PathModel.HoleOrBlock.Create(next, pathNumber).NextPoint;
            next = PathModel.RandomTripletAtLeastOne.Create(next, pathNumber).NextPoint;
            return new Grammar()
            {
                Part = PathPart.Chaos,
                NextPoint = next
            };
        }
    }
}