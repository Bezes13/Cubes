using Model;
using Path;
using UnityEngine;

namespace NonTerminals
{
    /// <summary>
    ///  NonTerminal Part which has 4 triple in a row, but could have an hole on the second or the third position
    /// </summary>
    public class BlockPart : NonTerminal
    {
        public BlockPart(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var next = PathModel.RandomTripletAtLeastOne.Create(start, pathNumber).NextPoint;
            next = PathModel.HoleOrBlock.Create(next, pathNumber).NextPoint;
            next = PathModel.HoleOrBlock.Create(next, pathNumber).NextPoint;
            next = PathModel.RandomTripletAtLeastOne.Create(next, pathNumber).NextPoint;
            return new Grammar
            {
                Part = PathPart.Chaos,
                NextPoint = next
            };
        }
    }
}