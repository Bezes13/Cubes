namespace NonTerminals
{
    public struct PieceProbability
    {
        public float Probablility;
        public PathPart Piece;

        public PieceProbability(float probablility, PathPart piece)
        {
            Piece = piece;
            Probablility = probablility;
        }
    }
}