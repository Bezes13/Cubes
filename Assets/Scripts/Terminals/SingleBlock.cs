using DefaultNamespace;
using UnityEngine;

namespace Terminals
{
    public class SingleBlock : Terminal
    {
        public override Vector3 Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            return start + new Vector3(0, 0, 1);
        }

        public SingleBlock(PathModel pathModel) : base(pathModel)
        {
            
        }
    }
}