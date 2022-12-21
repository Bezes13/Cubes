using DefaultNamespace;
using UnityEngine;

namespace Terminals
{
    public class UpStairs : Terminal
    {
        public override Vector3 Create(Vector3 start)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(0, 1, 1));
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(0, 1, 2));
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(0, 2, 3));
            return start + new Vector3(0, 2, 4);
        }

        public UpStairs(PathModel pathModel) : base(pathModel)
        {
        }
    }
}