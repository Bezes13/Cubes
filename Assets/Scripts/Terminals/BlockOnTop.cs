using Model;
using NonTerminals;
using UnityEngine;

namespace Terminals
{
    public class BlockOnTop : Terminal
    {
        public BlockOnTop(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.RandomTripletAtLeastOne.Create(start, pathNumber);
            PathModel.RandomTripletAtLeastOne.Create(start + Vector3.forward, pathNumber);
            PathModel.RandomTripletAtLeastOne.Create(start + Vector3.forward * 2, pathNumber);
            PathModel.RandomTripletAtLeastOne.Create(start + Vector3.forward * 3, pathNumber);
            float rnd = Random.value;
            PathModel.CreateObject(PathModel.Prefabtype.CubeOnTop, start + Vector3.up + (rnd < 0.33 ? Vector3.zero : rnd < 0.66 ? Vector3.right : Vector3.left), pathNumber);
            return new Grammar()
            {
                NextPoint = start + Vector3.forward * 4, 
                Part = PathPart.PreferBlockOnTop
            };
        }
    }
}