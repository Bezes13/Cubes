using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NonTerminals
{
    // NonTerminals are parts of the grammar, which resolve to further NonTerminals or Terminals
    public abstract class NonTerminal
    {
        protected readonly PathModel PathModel;
        private float _difficult;

        protected NonTerminal(PathModel pathModel)
        {
            PathModel = pathModel;
        }

        public void IncreaseDifficult(float value)
        {
            _difficult += value;
        }

        public abstract Grammar Create(Vector3 start, int pathNumber);

        protected PathPart GetNewPiece(List<PieceProbability> probabilities)
        {
            var rnd = Random.value;
            var switchCase = PathPart.TripleBlock;
            var check = 0f;
            foreach (var piece in probabilities)
            {
                check += (piece.Probability + _difficult * (piece.Max - piece.Probability)) / 100.0f;
                if (rnd < check)
                {
                    switchCase = piece.Piece;
                    break;
                }
            }

            return switchCase;
        }
    }
}