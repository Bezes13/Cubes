using NonTerminals;

namespace Path
{
    public readonly struct PieceProbability
    {
        public readonly float Probability;
        public readonly float Max;
        public readonly PathPart Piece;

        public PieceProbability(float probability, float max, PathPart piece)
        {
            Piece = piece;
            Probability = probability;
            Max = max;
        }
    }
}