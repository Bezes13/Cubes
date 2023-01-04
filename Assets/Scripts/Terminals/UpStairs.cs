using DefaultNamespace;
using Model;
using UnityEngine;

namespace Terminals
{
    public class UpStairs : Terminal
    {
        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(0, 1, 1), pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(0, 1, 2), pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start + new Vector3(0, 2, 3), pathNumber);
            return start + new Vector3(0, 2, 4);
        }

        public UpStairs(PathModel pathModel) : base(pathModel)
        {
        }
    }
}