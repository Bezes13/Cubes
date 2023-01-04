using Model;
using UnityEngine;

namespace NonTerminals
{
    public class JustTriplets : NonTerminal
    {
        private const int Elements = 1;
        public JustTriplets(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            int i = 0;
            Vector3 next = start;
            while (i <= Elements)
            {
                next = PathModel.BlockPart.Create(next, pathNumber);
                i++;
            }
            
            return PathModel.LineOrChaos.Create(next, pathNumber);
        }
    }
}