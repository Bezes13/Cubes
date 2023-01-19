using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    // NonTerminal Part which doesn't resolve to an Hole or an Spike
    public class NoHoleOrSpike : NonTerminal
    {
        public NoHoleOrSpike(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(1, 1, PathPart.LeftSweep),
                new PieceProbability(1, 1, PathPart.RightSweep),
                new PieceProbability(2, 2, PathPart.UpStairs),
                new PieceProbability(8,3, PathPart.RandomLog),
                new PieceProbability(1, 1, PathPart.Star),
                new PieceProbability(20, 82, PathPart.RandomTripleAtLeastOne)
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}