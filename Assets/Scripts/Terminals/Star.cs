using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates a collectable Star in the air, without pushing the nextPoint forward.
    /// </summary>
    public class Star : Terminal
    {
        public Star(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var rnd = Random.value;
            PathModel.CreatePathObject(PathModel.PrefabType.Star,
                start + new Vector3(0, 2, 0) + (rnd < 0.33 ? Vector3.zero : rnd < 0.66 ? Vector3.right : Vector3.left),
                pathNumber);
            return new Grammar
            {
                NextPoint = start,
                Part = PathPart.AfterSpikeOrHole
            };
        }
    }
}