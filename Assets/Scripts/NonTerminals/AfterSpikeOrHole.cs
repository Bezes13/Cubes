using System;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

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
                rnd <= 0.25 ? PathPart.UpStairs :
                rnd <= 0.5 ? PathPart.BlockOnTop :
                rnd <= 0.99 ? PathPart.TripleBlock : PathPart.Star; 


            switch (switchCase)
            {
                case PathPart.Hole: 
                    start = PathModel.Hole.Create(start, pathNumber).NextPoint;
                    return new Grammar {Part = PathPart.NoHoleNoSpike, NextPoint = start };
                case PathPart.SingleSpike: 
                    start = PathModel.SingleSpike.Create(start, pathNumber).NextPoint;
                    return new Grammar {Part = PathPart.NoHoleNoSpike, NextPoint = start };
                default:
                    return new Grammar {Part = switchCase, NextPoint = start };
            }
        }
    }
}