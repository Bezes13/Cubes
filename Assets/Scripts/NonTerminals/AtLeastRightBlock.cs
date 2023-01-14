using System.Collections.Generic;
using Model;
using UnityEngine;

namespace NonTerminals
{
    public class AtLeastRightBlock : NonTerminal
    {
        public AtLeastRightBlock(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            List<PieceProbability> probabilities = new List<PieceProbability>();
            probabilities.Add(new PieceProbability(0, 80, PathPart.RightBlock));
            probabilities.Add(new PieceProbability(30, 10, PathPart.RightMiddleBlock));
            probabilities.Add(new PieceProbability(40, 0, PathPart.TripleBlock));
            probabilities.Add(new PieceProbability(30, 10, PathPart.LeftRightBlock));
            
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}