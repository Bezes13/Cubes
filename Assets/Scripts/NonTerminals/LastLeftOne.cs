using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    /// NonTerminal Part which has a higher Chance to have Block on the left side
    /// </summary>
    public class LastLeftOne : NonTerminal
    {
        public LastLeftOne(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(70, 70, PathPart.AtLeastLeftBlock),
                new PieceProbability(30, 30, PathPart.Chaos)
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start};
        }
    }
}