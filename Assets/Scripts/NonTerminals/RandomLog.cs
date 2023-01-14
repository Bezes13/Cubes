using System.Collections.Generic;
using Model;
using UnityEngine;

namespace NonTerminals
{
    public class RandomLog : NonTerminal
    {
        public RandomLog(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            List<PieceProbability> probabilities = new List<PieceProbability>
            {
                new PieceProbability(33, 40, PathPart.RightLog),
                new PieceProbability(33, 40, PathPart.LeftLog),
                new PieceProbability(34, 20, PathPart.Log),
            };
            var switchCase = GetNewPiece(probabilities);
            Debug.Log(switchCase);
            return new Grammar {Part = switchCase, NextPoint = start };
        }
    }
}