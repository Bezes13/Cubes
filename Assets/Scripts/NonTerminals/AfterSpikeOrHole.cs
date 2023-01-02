using UnityEngine;

namespace NonTerminals
{
    public class AfterSpikeOrHole : NonTerminal
    {
        public AfterSpikeOrHole(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start)
        {
            var rnd = Random.value;
            var switchCase = rnd <= 0.05 ? PathPart.LeftSweep :
                rnd <= 0.1 ? PathPart.RightSweep :
                rnd <= 0.15 ? PathPart.Hole :
                rnd <= 0.2 ? PathPart.SingleSpike :
                rnd <= 0.25 ? PathPart.SingleBlock :
                rnd <= 0.3 ? PathPart.UpStairs :
                rnd <= 0.35 ? PathPart.JustTriples : PathPart.TripleBlock; 


            switch (switchCase)
            {
                case PathPart.LeftSweep: 
                    start = PathModel.LeftSweep.Create(start);
                    return PathModel.AfterSweep.Create(start);
                case PathPart.RightSweep: 
                    start = PathModel.RightSweep.Create(start);
                    return PathModel.AfterSweep.Create(start);
                case PathPart.Hole: 
                    start = PathModel.Hole.Create(start);
                    return PathModel.NoHoleOrSpike.Create(start);
                case PathPart.SingleSpike: 
                    start = PathModel.SingleSpike.Create(start);
                    return PathModel.NoHoleOrSpike.Create(start);
                case PathPart.SingleBlock: 
                    start = PathModel.SingleBlock.Create(start);
                    return start;
                case PathPart.TripleBlock: 
                    start = PathModel.TripleBlock.Create(start);
                    return start;
                case PathPart.UpStairs: 
                    start = PathModel.UpStairs.Create(start);
                    return start;
                case PathPart.JustTriples:
                    start = PathModel.JustTriplets.Create(start);
                    return PathModel.LineOrChaos.Create(start);
            }

            return start;
        }
    }
}