using Model;
using UnityEngine;

namespace NonTerminals
{
    public class HoleOrBlock : NonTerminal
    {
        public HoleOrBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            return Random.value > 0.9
                ? new Grammar {Part = PathPart.Hole, NextPoint = start}
                : new Grammar {Part = PathPart.RandomTripleAtLeastOne, NextPoint = start};
        }
    }
}