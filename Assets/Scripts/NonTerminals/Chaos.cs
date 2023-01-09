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
        RandomTripleAtLeastOne
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
                rnd <= 0.3 ? PathPart.PathSplitter : PathPart.TripleBlock;
            

            switch (switchCase)
            {
                case PathPart.LeftSweep: 
                    return new Grammar {Part = PathPart.LeftSweep, NextPoint = start };
                
                case PathPart.RightSweep: 
                    return new Grammar {Part = PathPart.RightSweep, NextPoint = start };
                case PathPart.Hole: 
                    return new Grammar {Part = PathPart.Hole, NextPoint = start };
                case PathPart.SingleSpike: 
                    return new Grammar {Part = PathPart.SingleSpike, NextPoint = start };
                case PathPart.TripleBlock: 
                    return new Grammar {Part = PathPart.TripleBlock, NextPoint = start };
                case PathPart.UpStairs: 
                    return new Grammar {Part = PathPart.UpStairs, NextPoint = start };
                case PathPart.PathSplitter:
                    return new Grammar {Part = PathPart.PathSplitter, NextPoint = start };
            }

            return new Grammar();
        }
    }
}