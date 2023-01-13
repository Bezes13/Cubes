using System;
using System.Collections.Generic;
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
            List<PieceProbability> probabilities = new List<PieceProbability>();
            probabilities.Add(new PieceProbability(3, PathPart.LeftSweep));
            probabilities.Add(new PieceProbability(3, PathPart.RightSweep));
            probabilities.Add(new PieceProbability(5, PathPart.SingleSpike));
            probabilities.Add(new PieceProbability(3, PathPart.UpStairs));
            probabilities.Add(new PieceProbability(10, PathPart.BlockOnTop));
            probabilities.Add(new PieceProbability(1, PathPart.Star));
            var switchCase = GetNewPiece(probabilities);


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