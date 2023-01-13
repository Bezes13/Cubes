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
            PathModel.CreateObject(PathModel.Prefabtype.Star, start + new Vector3(0, 2,0), pathNumber);
            return new Grammar()
            {
                NextPoint = start, 
                Part = PathPart.AfterSpikeOrHole
            };
        }
    }
}