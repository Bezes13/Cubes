using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    public class Hole : Terminal
    {
        public Hole(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            return new Grammar()
            {
                NextPoint = start + Vector3.forward,
                Part = PathPart.AfterSpikeOrHole
            };
        }
        
    }
}