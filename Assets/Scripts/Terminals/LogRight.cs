using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates a Log on the right side and places triple blocks under the log with at least one on the right
    /// </summary>
    public class LogRight : Terminal
    {
        public LogRight(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var gen = PathModel.GetGeneratorByNumber(pathNumber);
            gen.CreateInBetween(PathModel.AtLeastRightBlock.Create(start, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastRightBlock.Create(start + Vector3.forward, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastRightBlock.Create(start + Vector3.forward * 2, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastRightBlock.Create(start + Vector3.forward * 3, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastRightBlock.Create(start + Vector3.forward * 4, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastRightBlock.Create(start + Vector3.forward * 5, pathNumber));
            PathModel.CreatePathObject(PathModel.PrefabType.Log, start + new Vector3(1, 1, 1), pathNumber);
            return new Grammar
            {
                NextPoint = start + Vector3.forward * 6,
                Part = PathPart.Chaos
            };
        }
    }
}