using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    /// NonTerminal Part which resolves to a triple Part, but always has a Block on the right side
    /// </summary>
    public class AtLeastRightBlock : NonTerminal
    {
        public AtLeastRightBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(0, 80, PathPart.RightBlock),
                new PieceProbability(30, 10, PathPart.RightMiddleBlock),
                new PieceProbability(40, 0, PathPart.TripleBlock),
                new PieceProbability(30, 10, PathPart.LeftRightBlock)
            };

            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start};
        }
    }
}