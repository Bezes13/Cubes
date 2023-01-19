using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    /// NonTerminal Part which resolves to any combination of a triple block
    /// </summary>
    public class RandomTripletAtLeastOne : NonTerminal
    {
        public RandomTripletAtLeastOne(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(3, 30, PathPart.LeftBlock),
                new PieceProbability(3, 30, PathPart.RightBlock),
                new PieceProbability(30, 3, PathPart.LeftMiddleBlock),
                new PieceProbability(30, 3, PathPart.RightMiddleBlock),
                new PieceProbability(30, 4, PathPart.LeftRightBlock),
                new PieceProbability(4, 30, PathPart.SingleBlock)
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start};
        }
    }
}