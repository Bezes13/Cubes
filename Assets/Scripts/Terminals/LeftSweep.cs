using DefaultNamespace;
using UnityEngine;

namespace Terminals
{
    public class LeftSweep : Terminal
    {
        public LeftSweep(PathModel pathModel) : base(pathModel)
        {
        }

        public override Vector3 Create(Vector3 start)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.left);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + Vector3.left * 2);
            return start + Vector3.left * 2;
        }
    }
}