using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    public class AtLeastLeftBlock : NonTerminal
    {
        public AtLeastLeftBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(0, 80, PathPart.LeftBlock),
                new PieceProbability(30, 10, PathPart.LeftMiddleBlock),
                new PieceProbability(40, 0, PathPart.TripleBlock),
                new PieceProbability(30, 10, PathPart.LeftRightBlock)
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}