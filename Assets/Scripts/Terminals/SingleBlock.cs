using DefaultNamespace;
using Model;
using UnityEngine;

namespace Terminals
{
    public class SingleBlock : Terminal
    {
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            return new Grammar() {NextPoint = start + Vector3.forward};
        }

        public SingleBlock(PathModel pathModel) : base(pathModel)
        {
            
        }
    }
}