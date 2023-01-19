using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    /// NonTerminal Part which resolves to a Log an any side
    /// </summary>
    public class RandomLog : NonTerminal
    {
        public RandomLog(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var probabilities = new List<PieceProbability>
            {
                new PieceProbability(33, 40, PathPart.RightLog),
                new PieceProbability(33, 40, PathPart.LeftLog),
                new PieceProbability(34, 20, PathPart.Log),
            };
            var switchCase = GetNewPiece(probabilities);
            return new Grammar {Part = switchCase, NextPoint = start};
        }
    }
}