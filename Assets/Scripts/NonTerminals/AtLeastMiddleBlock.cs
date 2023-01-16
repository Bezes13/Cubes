using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    public class AtLeastMiddleBlock : NonTerminal
    {
        public AtLeastMiddleBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            List<PieceProbability> probabilities = new List<PieceProbability>();
            probabilities.Add(new PieceProbability(0, 80, PathPart.SingleBlock));
            probabilities.Add(new PieceProbability(30, 10, PathPart.LeftMiddleBlock));
            probabilities.Add(new PieceProbability(40, 0, PathPart.TripleBlock));
            probabilities.Add(new PieceProbability(30, 10, PathPart.RightMiddleBlock));
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}