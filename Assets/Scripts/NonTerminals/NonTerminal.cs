using System.Collections.Generic;
using Model;
using UnityEngine;

namespace NonTerminals
{
    public abstract class NonTerminal
    {
        protected PathModel PathModel;

        public NonTerminal(PathModel pathModel)
        {
            PathModel = pathModel;
        }

        public abstract Grammar Create(Vector3 start, int pathNumber);
        
        protected static PathPart GetNewPiece(List<PieceProbability> probabilities)
        {
            var rnd = Random.value;
            var switchCase = PathPart.TripleBlock;
            var check = 0f;
            foreach (var piece in probabilities)
            {
                check += piece.Probablility / 100.0f;
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