using System.Collections.Generic;
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
            List<PieceProbability> probabilities = new List<PieceProbability>();
            probabilities.Add(new PieceProbability(1, 1, PathPart.LeftSweep));
            probabilities.Add(new PieceProbability(1, 1, PathPart.RightSweep));
            probabilities.Add(new PieceProbability(2, 2, PathPart.UpStairs));
            probabilities.Add(new PieceProbability(8,3, PathPart.RandomLog));
            probabilities.Add(new PieceProbability(1, 1, PathPart.Star));
            probabilities.Add(new PieceProbability(20, 82, PathPart.RandomTripleAtLeastOne));
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}