namespace NonTerminals
{
    /// <summary>
    /// PathParts represent Terminals and NonTerminals
    /// </summary>
    public enum PathPart
    {
        LeftSweep,
        RightSweep,
        Hole,
        SingleSpike,
        SingleBlock,
        TripleBlock,
        UpStairs,
        Chaos,
        PathSplitter,
        AfterSweep,
        NoHoleNoSpike,
        AfterSpikeOrHole,
        RandomTripleAtLeastOne,
        Log,
        LeftLog,
        RightLog,
        RandomLog,
        Star,
        AfterStairs,
        LeftBlock,
        RightBlock,
        LeftRightBlock,
        LeftMiddleBlock,
        RightMiddleBlock,
        AtLeastMiddleBlock,
        AtLeastRightBlock,
        AtLeastLeftBlock,
        LastRightOne,
        LastLeftOne,
        LastMiddleOne
    }
}