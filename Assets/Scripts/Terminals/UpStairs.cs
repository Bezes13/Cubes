using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates upstairs
    /// </summary>
    public class UpStairs : Terminal
    {
        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + new Vector3(0, 0, 1), pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + new Vector3(0, 1, 1), pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + new Vector3(0, 1, 2), pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start + new Vector3(0, 2, 3), pathNumber);
            return new Grammar
            {
                NextPoint = start + new Vector3(0, 2, 4),
                Part = PathPart.AfterStairs
            };
        }

        public UpStairs(PathModel pathModel) : base(pathModel)
        {
        }
    }
}