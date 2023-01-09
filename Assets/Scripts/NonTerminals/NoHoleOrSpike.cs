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
            var switchCase = rnd <= 0.05 ? PathPart.LeftSweep :
                rnd <= 0.1 ? PathPart.RightSweep :
                rnd <= 0.15 ? PathPart.UpStairs : PathPart.TripleBlock; 

            switch (switchCase)
            {
                case PathPart.LeftSweep: 
                    return new Grammar {Part = PathPart.LeftSweep, NextPoint = start};
                case PathPart.RightSweep: 
                    return new Grammar {Part = PathPart.RightSweep, NextPoint = start};
                case PathPart.TripleBlock: 
                    return new Grammar {Part = PathPart.TripleBlock, NextPoint = start};
                case PathPart.UpStairs: 
                    return new Grammar {Part = PathPart.UpStairs, NextPoint = start};
            }

            return new Grammar(){NextPoint = start};
        }
    }
}