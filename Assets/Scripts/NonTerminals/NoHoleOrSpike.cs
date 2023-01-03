using UnityEngine;

namespace NonTerminals
{
    public class NoHoleOrSpike : NonTerminal
    {
        public NoHoleOrSpike(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            var rnd = Random.value;
            var switchCase = rnd <= 0.05 ? PathPart.LeftSweep :
                rnd <= 0.1 ? PathPart.RightSweep :
                rnd <= 0.25 ? PathPart.SingleBlock :
                rnd <= 0.2 ? PathPart.UpStairs :
                rnd <= 0.15 ? PathPart.JustTriples : PathPart.TripleBlock; 

            switch (switchCase)
            {
                case PathPart.LeftSweep: 
                    start = PathModel.LeftSweep.Create(start, pathNumber);
                    return PathModel.AfterSweep.Create(start, pathNumber);
                case PathPart.RightSweep: 
                    start = PathModel.RightSweep.Create(start, pathNumber);
                    return PathModel.AfterSweep.Create(start, pathNumber);
                case PathPart.SingleBlock: 
                    start = PathModel.SingleBlock.Create(start, pathNumber);
                    return start;
                case PathPart.TripleBlock: 
                    start = PathModel.TripleBlock.Create(start, pathNumber);
                    return start;
                case PathPart.UpStairs: 
                    start = PathModel.UpStairs.Create(start, pathNumber);
                    return start;
                case PathPart.JustTriples:
                    start = PathModel.JustTriplets.Create(start, pathNumber);
                    return PathModel.LineOrChaos.Create(start, pathNumber);
            }

            return start;
        }
    }
}