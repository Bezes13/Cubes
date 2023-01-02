using UnityEngine;

namespace NonTerminals
{
    public class JustTriplets : NonTerminal
    {
        private const int Elements = 4;
        public JustTriplets(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start)
        {
            int i = 0;
            Vector3 next = start;
            while (i <= Elements)
            {
                next = PathModel.BlockPart.Create(next);
                i++;
            }
            
            return PathModel.LineOrChaos.Create(next);
        }
    }
}