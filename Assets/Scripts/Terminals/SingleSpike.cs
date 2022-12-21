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

        public override Vector3 Create(Vector3 start)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start);
            PathModel.CreateObject(PathModel.Prefabtype.Pyramid, start + new Vector3(0, 0.4f,0));
            return start + Vector3.forward;
        }
    }
}