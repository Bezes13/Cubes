using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    /// NonTerminal Part which resolves either to triple Block or an Hole
    /// </summary>
    public class HoleOrBlock : NonTerminal
    {
        public HoleOrBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var difficulty = 0.95f + -0.95f * Difficult;
            return Random.value > difficulty
                ? new Grammar {Part = PathPart.Hole, NextPoint = start}
                : new Grammar {Part = PathPart.RandomTripleAtLeastOne, NextPoint = start};
        }
    }
}