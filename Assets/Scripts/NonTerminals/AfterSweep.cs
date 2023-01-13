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
                rnd <= 0.15 ? PathPart.UpStairs :
                rnd <= 0.5 ? PathPart.BlockOnTop :
                rnd <= 0.99 ? PathPart.TripleBlock : PathPart.Star;

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}