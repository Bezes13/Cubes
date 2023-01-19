using System.Collections.Generic;
using Model;
using Path;
using Terminals;
using UnityEngine;

namespace NonTerminals
{
    public class AfterSweep : NonTerminal
    {
        /// <summary>
        /// NonTerminal part of the Grammar which can be resolved to several Terminals, which occurs after a
        /// <see cref="LeftSweep"/> or a <see cref="RightSweep"/>.
        /// </summary>
        public AfterSweep(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(2, 4, PathPart.Hole),
                new PieceProbability(2, 4, PathPart.SingleSpike),
                new PieceProbability(5, 5, PathPart.UpStairs),
                new PieceProbability(8, 3, PathPart.RandomLog),
                new PieceProbability(1, 1, PathPart.Star),
                new PieceProbability(40, 80, PathPart.RandomTripleAtLeastOne)
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start};
        }
    }
}