using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    /// NonTerminal which can get resolved to any Part
    /// </summary>
    public class Chaos : NonTerminal
    {
        public Chaos(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(5, 5, PathPart.LeftSweep),
                new PieceProbability(5, 5, PathPart.RightSweep),
                new PieceProbability(5, 15, PathPart.SingleSpike),
                new PieceProbability(3, 3, PathPart.UpStairs),
                new PieceProbability(2, 1, PathPart.PathSplitter),
                new PieceProbability(5, 15, PathPart.Hole),
                new PieceProbability(2, 2, PathPart.Star),
                new PieceProbability(20, 50, PathPart.RandomTripleAtLeastOne),
                new PieceProbability(7, 2, PathPart.RandomLog)
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start};
        }
    }
}