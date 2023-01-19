using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which moves the nextPoint one unit further without creating something
    /// </summary>
    public class Hole : Terminal
    {
        public Hole(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            return new Grammar
            {
                NextPoint = start + Vector3.forward,
                Part = PathPart.AfterSpikeOrHole
            };
        }
    }
}