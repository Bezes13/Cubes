using System.Collections.Generic;
using Model;
using UnityEngine;

namespace NonTerminals
{
    public class Chaos : NonTerminal
    {
        public Chaos(PathModel pathModel) : base(pathModel)
        {
        }
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            List<PieceProbability> probabilities = new List<PieceProbability>();
            probabilities.Add(new PieceProbability(5,5, PathPart.LeftSweep));
            probabilities.Add(new PieceProbability(5,5, PathPart.RightSweep));
            probabilities.Add(new PieceProbability(5,15, PathPart.SingleSpike));
            probabilities.Add(new PieceProbability(3,3, PathPart.UpStairs));
            probabilities.Add(new PieceProbability(2, 1, PathPart.PathSplitter));
            probabilities.Add(new PieceProbability(5, 15, PathPart.Hole));
            probabilities.Add(new PieceProbability(1,1, PathPart.Star));
            probabilities.Add(new PieceProbability(20,50, PathPart.RandomTripleAtLeastOne));
            probabilities.Add(new PieceProbability(8,3, PathPart.RandomLog));
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}