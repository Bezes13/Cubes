using System.Collections.Generic;
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
            List<PieceProbability> probabilities = new List<PieceProbability>();
            probabilities.Add(new PieceProbability(5, 5, PathPart.LeftSweep));
            probabilities.Add(new PieceProbability(5, 5, PathPart.RightSweep));
            probabilities.Add(new PieceProbability(2, 10, PathPart.UpStairs));
            probabilities.Add(new PieceProbability(8, 3, PathPart.RandomLog));
            probabilities.Add(new PieceProbability(1, 1, PathPart.Star));
            probabilities.Add(new PieceProbability(27, 74, PathPart.RandomTripleAtLeastOne));
            probabilities.Add(new PieceProbability(40, 0, PathPart.TripleBlock));
            var switchCase = GetNewPiece(probabilities);

            var gen = PathModel.GetGeneratorByNumber(pathNumber);
            switch (switchCase)
            {
                case PathPart.Hole: 
                    gen.CreateInBetween(PathModel.Hole.Create(start, pathNumber));
                    return new Grammar {Part = PathPart.NoHoleNoSpike, NextPoint = start + Vector3.forward };
                case PathPart.SingleSpike: 
                    gen.CreateInBetween(PathModel.SingleSpike.Create(start, pathNumber));
                    return new Grammar {Part = PathPart.NoHoleNoSpike, NextPoint = start + Vector3.forward };
                default:
                    return new Grammar {Part = switchCase, NextPoint = start };
            }
        }
    }
}