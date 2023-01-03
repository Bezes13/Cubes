using UnityEngine;

namespace NonTerminals
{
    public class LineOrChaos : NonTerminal
    {
        public LineOrChaos(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            var rnd = Random.value;
            var switchCase = rnd <= 0.7 ? PathPart.Chaos : PathPart.JustTriples; 

            switch (switchCase)
            {
                case PathPart.Chaos:
                    return start;
                case PathPart.JustTriples:
                    return PathModel.JustTriplets.Create(start, pathNumber);
            }

            return start;
        }
    }
}