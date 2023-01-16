using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    public class AfterStairs : NonTerminal
    {
        public AfterStairs(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            List<PieceProbability> probabilities = new List<PieceProbability>
            {
                new PieceProbability(5, 10, PathPart.SingleSpike),
                new PieceProbability(5, 10, PathPart.Hole),
                new PieceProbability(20, 80, PathPart.RandomTripleAtLeastOne)
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}