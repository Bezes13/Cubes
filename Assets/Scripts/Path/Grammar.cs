using NonTerminals;
using UnityEngine;

namespace Path
{
    /// <summary>
    /// Object of the Grammar, where the given PathPart should get placed on the NextPoint
    /// </summary>
    public struct Grammar
    {
        public Vector3 NextPoint;
        public PathPart Part;
    }
}