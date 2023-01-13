using Model;
using UnityEngine;

namespace NonTerminals
{
    public class NoHoleOrSpike : NonTerminal
    {
        public NoHoleOrSpike(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var rnd = Random.value;
            var switchCase = rnd <= 0.05 ? PathPart.LeftSweep :
                rnd <= 0.1 ? PathPart.RightSweep :
                rnd <= 0.15 ? PathPart.UpStairs : 
                rnd <= 0.5 ? PathPart.BlockOnTop :
                rnd <= 0.99 ? PathPart.TripleBlock : PathPart.Star;

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}