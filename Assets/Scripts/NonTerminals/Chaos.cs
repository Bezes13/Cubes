using System;
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
        Star
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
            var rnd = Random.value;
            var switchCase = rnd <= 0.05 ? PathPart.LeftSweep :
                rnd <= 0.1 ? PathPart.RightSweep :
                rnd <= 0.15 ? PathPart.Hole :
                rnd <= 0.2 ? PathPart.SingleSpike :
                rnd <= 0.25 ? PathPart.UpStairs :
                rnd <= 0.27 ? PathPart.PathSplitter : 
                rnd <= 0.5 ? PathPart.BlockOnTop :
                rnd <= 0.99 ? PathPart.TripleBlock : PathPart.Star;

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}