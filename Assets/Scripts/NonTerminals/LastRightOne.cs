using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    /// NonTerminal Part which has a higher Chance to have Block on the right side
    /// </summary>
    public class LastRightOne : NonTerminal
    {
        public LastRightOne(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(60, 60, PathPart.AtLeastRightBlock),
                new PieceProbability(30, 30, PathPart.Chaos),
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start};
        }
    }
}