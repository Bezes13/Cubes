using DefaultNamespace;
using UnityEngine;

namespace Terminals
{
    // This class places a pyramid representing a SingleSpike on a Cube, if the player collides with the pyramid, he loses.
    // The player needs to jump over it, or next to it
    public class SingleSpike : Terminal
    {
        public SingleSpike(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Pyramid, start + new Vector3(0, 0.5f,0), pathNumber);
            return start + Vector3.forward;
        }
    }
}