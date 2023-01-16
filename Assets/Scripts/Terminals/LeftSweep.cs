using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    public class LeftSweep : Terminal
    {
        public LeftSweep(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.PrefabType.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.PrefabType.Cube, start + Vector3.left, pathNumber);
            PathModel.CreateObject(PathModel.PrefabType.Cube, start + Vector3.left * 2, pathNumber);
            PathModel.CreateObject(PathModel.PrefabType.Cube, start + Vector3.left * 3, pathNumber);
            return new Grammar() {NextPoint = start + Vector3.left * 3 + Vector3.forward, Part = PathPart.AfterSweep};
        }
    }
}