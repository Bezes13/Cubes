using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NonTerminals
{
    public enum PathPart
    {
        LeftSweep,
        RightSweep,
        Hole,
        SingleSpike,
        SingleBlock, 
        TripleBlock,
        UpStairs,
        Chaos,
        PathSplitter,
        AfterSweep,
        NoHoleNoSpike,
        AfterSpikeOrHole,
        RandomTripleAtLeastOne,
        PreferBlockOnTop,
        BlockOnTop,
        Star,
        AfterStairs
    }
    
    public class Chaos : NonTerminal
    {
        private Type lastType;
        private int countLastType;
        
        public Chaos(PathModel pathModel) : base(pathModel)
        {
        }
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            List<PieceProbability> probabilities = new List<PieceProbability>();
            probabilities.Add(new PieceProbability(5, PathPart.LeftSweep));
            probabilities.Add(new PieceProbability(5, PathPart.RightSweep));
            probabilities.Add(new PieceProbability(5, PathPart.SingleSpike));
            probabilities.Add(new PieceProbability(3, PathPart.UpStairs));
            probabilities.Add(new PieceProbability(10, PathPart.BlockOnTop));
            probabilities.Add(new PieceProbability(2, PathPart.PathSplitter));
            probabilities.Add(new PieceProbability(5, PathPart.Hole));
            probabilities.Add(new PieceProbability(10, PathPart.Star));
            probabilities.Add(new PieceProbability(20, PathPart.RandomTripleAtLeastOne));
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}