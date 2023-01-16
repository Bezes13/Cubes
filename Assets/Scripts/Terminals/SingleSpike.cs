using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    // This class places a pyramid representing a SingleSpike on a Cube, if the player collides with the pyramid, he loses.
    // The player needs to jump over it, or next to it
    public class SingleSpike : Terminal
    {
        public SingleSpike(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreateObject(PathModel.PrefabType.Cube, start, pathNumber);
            PathModel.CreateObject(PathModel.PrefabType.Pyramid, start + new Vector3(0, 1,0), pathNumber);
            return new Grammar
            {
                NextPoint = start + Vector3.forward, 
                Part = PathPart.AfterSpikeOrHole
            };
        }
    }
}