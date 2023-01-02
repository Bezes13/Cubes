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

        public abstract Vector3 Create(Vector3 start);
    }
}