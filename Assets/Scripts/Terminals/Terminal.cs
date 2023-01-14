using Model;
using UnityEngine;

namespace Terminals
{
    public abstract class Terminal
    {
        protected PathModel PathModel;

        protected Terminal(PathModel pathModel)
        {
            PathModel = pathModel;
        }

        public abstract Grammar Create(Vector3 start, int pathNumber);
    }
}