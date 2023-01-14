using System.Collections.Generic;
using Model;
using NonTerminals;
using UnityEngine;
namespace Terminals
{
    public class RandomTripletAtLeastOne : NonTerminal
    {
        private float _difficulty;

        public RandomTripletAtLeastOne(PathModel pathModel, float difficulty) : base(pathModel)
        {
            _difficulty = difficulty;
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            List<PieceProbability> probabilities = new List<PieceProbability>
            {
                new PieceProbability(3, 30, PathPart.LeftBlock),
                new PieceProbability(3, 30, PathPart.RightBlock),
                new PieceProbability(30, 3, PathPart.LeftMiddleBlock),
                new PieceProbability(30, 3, PathPart.RightMiddleBlock),
                new PieceProbability(30, 4, PathPart.LeftRightBlock),
                new PieceProbability(4, 30, PathPart.SingleBlock)
            };
            var switchCase = GetNewPiece(probabilities);

            return new Grammar {Part = switchCase, NextPoint = start };
        }

        public void Increase(float difficulty)
        {
            _difficulty = difficulty;
        }
    }
}