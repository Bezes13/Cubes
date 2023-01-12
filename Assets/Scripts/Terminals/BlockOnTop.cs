using Model;
using NonTerminals;
using UnityEngine;

namespace Terminals
{
    class BlockOnTop : Terminal
    {
        public BlockOnTop(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.Prefabtype.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.Prefabtype.CubeOnTop, start + new Vector3(0, 1,0), pathNumber);
            return new Grammar()
            {
                NextPoint = start + Vector3.forward, 
                Part = PathPart.PreferBlockOnTop
            };
        }
    }
}