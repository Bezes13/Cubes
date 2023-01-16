using NonTerminals;

namespace Path
{
    public struct PieceProbability
    {
        public readonly float Probablility;
        public readonly float Max;
        public readonly PathPart Piece;

        public PieceProbability(float probablility, float max, PathPart piece)
        {
            Piece = piece;
            Probablility = probablility;
            Max = max;
        }
    }
}