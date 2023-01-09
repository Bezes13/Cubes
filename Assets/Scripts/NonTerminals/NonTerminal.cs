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
    }
}