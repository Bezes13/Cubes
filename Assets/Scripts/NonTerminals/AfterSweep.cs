using Model;
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
            var rnd = Random.value;
            var switchCase = rnd <= 0.05 ? PathPart.Hole :
                rnd <= 0.1 ? PathPart.SingleSpike :
                rnd <= 0.15 ? PathPart.UpStairs : PathPart.TripleBlock; 

            switch (switchCase)
            {
                case PathPart.Hole: 
                    return new Grammar {Part = PathPart.Hole, NextPoint = start };
                case PathPart.SingleSpike: 
                    return new Grammar {Part = PathPart.SingleSpike, NextPoint = start };
                case PathPart.TripleBlock: 
                    return new Grammar {Part = PathPart.TripleBlock, NextPoint = start };
                case PathPart.UpStairs: 
                    return new Grammar {Part = PathPart.UpStairs, NextPoint = start };
            }

            return new Grammar();
        }
    }
}