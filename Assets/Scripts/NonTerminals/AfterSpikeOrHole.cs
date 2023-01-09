using Model;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    /// NonTerminal part of the Grammar which can be resolved to several Terminals, which occurs after a Spike or a Hole.
    /// </summary>
    public class AfterSpikeOrHole : NonTerminal
    {
        public AfterSpikeOrHole(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var rnd = Random.value;
            var switchCase = rnd <= 0.05 ? PathPart.LeftSweep :
                rnd <= 0.1 ? PathPart.RightSweep :
                rnd <= 0.15 ? PathPart.Hole :
                rnd <= 0.2 ? PathPart.SingleSpike :
                rnd <= 0.25 ? PathPart.UpStairs : PathPart.TripleBlock; 


            switch (switchCase)
            {
                case PathPart.LeftSweep: 
                    return new Grammar() {Part = PathPart.LeftSweep, NextPoint = start};
                case PathPart.RightSweep: 
                    return new Grammar() {Part = PathPart.RightSweep, NextPoint = start + Vector3.forward};
                case PathPart.Hole: 
                    start = PathModel.Hole.Create(start, pathNumber).NextPoint;
                    return new Grammar {Part = PathPart.NoHoleNoSpike, NextPoint = start };
                case PathPart.SingleSpike: 
                    start = PathModel.SingleSpike.Create(start, pathNumber).NextPoint;
                    return new Grammar {Part = PathPart.NoHoleNoSpike, NextPoint = start };
                case PathPart.TripleBlock: 
                    return new Grammar {Part = PathPart.TripleBlock, NextPoint = start };
                case PathPart.UpStairs: 
                    return new Grammar {Part = PathPart.UpStairs, NextPoint = start };
            }

            return new Grammar();
        }
    }
}