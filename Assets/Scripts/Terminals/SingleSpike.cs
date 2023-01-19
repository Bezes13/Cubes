using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates a Spike on top of a cube, the player dies if he collides with the spike.
    /// </summary>
    public class SingleSpike : Terminal
    {
        public SingleSpike(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            PathModel.CreatePathObject(PathModel.PrefabType.Cube, start, pathNumber);
            PathModel.CreatePathObject(PathModel.PrefabType.Pyramid, start + new Vector3(0, 1,0), pathNumber);
            return new Grammar
            {
                NextPoint = start + Vector3.forward, 
                Part = PathPart.AfterSpikeOrHole
            };
        }
    }
}