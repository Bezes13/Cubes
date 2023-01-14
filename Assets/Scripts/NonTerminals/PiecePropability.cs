namespace NonTerminals
{
    public struct PieceProbability
    {
        public float Probablility;
        public float Max;
        public PathPart Piece;

        public PieceProbability(float probablility, float max, PathPart piece)
        {
            Piece = piece;
            Probablility = probablility;
            Max = max;
        }
    }
}