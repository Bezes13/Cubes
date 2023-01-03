using DefaultNamespace;
using UnityEngine;

namespace Terminals
{
    public abstract class Terminal
    {
        protected PathModel PathModel;

        public Terminal(PathModel pathModel)
        {
            PathModel = pathModel;
        }

        public abstract Vector3 Create(Vector3 start, int pathNumber);
    }
}