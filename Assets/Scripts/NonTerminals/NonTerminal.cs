using System.Collections.Generic;
using Model;
using Path;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NonTerminals
{
    public abstract class NonTerminal
    {
        protected PathModel PathModel;
        protected float Difficult;

        public NonTerminal(PathModel pathModel)
        {
            PathModel = pathModel;
        }

        public void IncreaseDifficult(float value)
        {
            Difficult += value;
        }

        public abstract Grammar Create(Vector3 start, int pathNumber);
        
        protected PathPart GetNewPiece(List<PieceProbability> probabilities)
        {
            var rnd = Random.value;
            var switchCase = PathPart.TripleBlock;
            var check = 0f;
            foreach (var piece in probabilities)
            {
                check += (piece.Probablility + Difficult * (piece.Max - piece.Probablility)) / 100.0f;
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