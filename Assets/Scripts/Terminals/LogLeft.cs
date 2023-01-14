using Model;
using NonTerminals;
using UnityEngine;

namespace Terminals
{
    public class LogLeft : Terminal
    {
        public LogLeft(PathModel pathModel) : base(pathModel)
        {
        }

        public override Grammar Create(Vector3 start, int pathNumber)
        {
            var gen = PathModel.GetGeneratorByNumber(pathNumber);
            gen.CreateInBetween(PathModel.AtLeastLeftBlock.Create(start, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastLeftBlock.Create(start + Vector3.forward, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastLeftBlock.Create(start + Vector3.forward * 2, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastLeftBlock.Create(start + Vector3.forward * 3, pathNumber));
            gen.CreateInBetween(PathModel.AtLeastLeftBlock.Create(start + Vector3.forward * 4, pathNumber));
            PathModel.CreateObject(PathModel.Prefabtype.Log, start + Vector3.forward + Vector3.up + Vector3.left, pathNumber);
            return new Grammar()
            {
                NextPoint = start + Vector3.forward * 5, 
                Part = PathPart.Chaos
            };
        }
    }
}