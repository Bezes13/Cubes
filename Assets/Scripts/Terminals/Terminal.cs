using Model;
using Path;
using UnityEngine;

namespace Terminals
{
    /// <summary>
    /// Terminals are part of the grammar which resolve directly to pathObjects which are directly created.
    /// </summary>
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