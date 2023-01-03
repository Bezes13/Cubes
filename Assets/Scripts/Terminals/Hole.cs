using UnityEngine;

namespace Terminals
{
    public class Hole : Terminal
    {
        public Hole(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            return start + Vector3.forward;
        }
        
    }
}