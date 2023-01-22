using Model;
using NonTerminals;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminal which creates a Log in the middle and places triple blocks under the log with at least one in the middle
    /// </summary>
    public class Log : Terminal
    {
        public Log(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var gen = PathModel.GetGeneratorByNumber(pathNumber);
            gen.CreateInBetween(PathModel.AtLeastMiddleBlock.Create(start, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastMiddleBlock.Create(start + Vector3.forward, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastMiddleBlock.Create(start + Vector3.forward * 2, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastMiddleBlock.Create(start + Vector3.forward * 3, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastMiddleBlock.Create(start + Vector3.forward * 4, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastMiddleBlock.Create(start + Vector3.forward * 5, pathNumber));
            PathModel.CreatePathObject(PathModel.PrefabType.Log, start + Vector3.forward + Vector3.up, pathNumber);
            return new Grammar
            {
                NextPoint = start + Vector3.forward * 6, 
                Part = PathPart.Chaos
            };
        }
    }
}