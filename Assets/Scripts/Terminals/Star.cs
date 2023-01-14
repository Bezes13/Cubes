using Model;
using NonTerminals;
using UnityEngine;

namespace Terminals
{
    public class Star : Terminal
    {
        public Star(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var rnd = Random.value;
            PathModel.CreateObject(PathModel.Prefabtype.Star, start + new Vector3(0, 2,0) + (rnd < 0.33 ? Vector3.zero : rnd < 0.66 ? Vector3.right : Vector3.left), pathNumber);
            return new Grammar()
            {
                NextPoint = start, 
                Part = PathPart.AfterSpikeOrHole
            };
        }
    }
}