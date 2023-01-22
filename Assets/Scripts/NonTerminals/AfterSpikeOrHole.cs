using System.Collections.Generic;
using Model;
using Path;
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
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(5, 5, PathPart.LeftSweep),
                new PieceProbability(5, 5, PathPart.RightSweep),
                new PieceProbability(6, 6, PathPart.Hole),
                new PieceProbability(6, 6, PathPart.SingleSpike),
                new PieceProbability(2, 10, PathPart.UpStairs),
                new PieceProbability(8, 3, PathPart.RandomLog),
                new PieceProbability(1, 1, PathPart.Star),
                new PieceProbability(27, 74, PathPart.RandomTripleAtLeastOne),
                new PieceProbability(40, 0, PathPart.TripleBlock)
            };
            var switchCase = GetNewPiece(probabilities);
            
            switch (switchCase)
            {
                case PathPart.Hole:
                    return new Grammar {Part = PathPart.NoHoleNoSpike, NextPoint = start + Vector3.forward};
                case PathPart.SingleSpike:
                    PathModel.SingleSpike.Create(start, pathNumber);
                    return new Grammar {Part = PathPart.NoHoleNoSpike, NextPoint = start + Vector3.forward};
                default:
                    return new Grammar {Part = switchCase, NextPoint = start};
            }
        }
    }
}