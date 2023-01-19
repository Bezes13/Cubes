using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    /// NonTerminal Part which has a higher Chance to have Block in the middle
    /// </summary>
    public class LastMidOne : NonTerminal
    {
        public LastMidOne(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(70, 70, PathPart.AtLeastMiddleBlock),
                new PieceProbability(30, 30, PathPart.Chaos)
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start};
        }
    }
}