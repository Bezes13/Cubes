using DefaultNamespace;
using UnityEngine;

namespace Terminals
{
    public class RightSweep : Terminal
    {
        public RightSweep(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.right);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.right * 2);
            return start + Vector3.right * 2;
        }
    }
}