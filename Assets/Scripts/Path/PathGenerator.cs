using System.Collections.Generic;
using Model;
using NonTerminals;
using Objects;
using Signals;
using UnityEngine;

namespace Path
{
    public class PathGenerator : MonoBehaviour
    {
        public PathModel pathModel;
        public Vector3 start;
        private Grammar _nextPoint;
        public int pathNumber;
        public List<Cube> cubes;
        public int sidePath;
        public PathModel.Prefabtype type;
        private bool _stopCreating;
        private Camera _camera;

        private void Awake()
        {
            cubes = new List<Cube>();
            Supyrb.Signals.Get<RestartGameSignal>().AddListener(CreatePath);
        }

        private void Start()
        {
            _camera = Camera.main;
            CreatePath();
        }

        private void OnDestroy()
        {
            Supyrb.Signals.Get<RestartGameSignal>().RemoveListener(CreatePath);
        }

        private void Update()
        {
            ContinuePath();
        }

        private void ContinuePath()
        {
            if (_stopCreating)
            {
                return;
            }

            var screenPoint = _camera.WorldToViewportPoint(_nextPoint.NextPoint);
            var onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 0.9;
            if (!onScreen)
            {
                return;
            }

            _nextPoint = SwitchCase(_nextPoint);
        }

        public void CreateInBetween(Grammar grammar)
        {
            SwitchCase(grammar);
        }

        private Grammar SwitchCase(Grammar grammar)
        {
            switch (grammar.Part)
            {
                case PathPart.RightSweep:
                    return pathModel.RightSweep.Create(grammar.NextPoint, pathNumber);

                case PathPart.LeftSweep:
                    return pathModel.LeftSweep.Create(grammar.NextPoint, pathNumber);

                case PathPart.Hole:
                    return pathModel.Hole.Create(grammar.NextPoint, pathNumber);

                case PathPart.SingleSpike:
                    return pathModel.SingleSpike.Create(grammar.NextPoint, pathNumber);

                case PathPart.TripleBlock:
                    return pathModel.TripleBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.UpStairs:
                    return pathModel.UpStairs.Create(grammar.NextPoint, pathNumber);

                case PathPart.AfterStairs:
                    return pathModel.AfterStairs.Create(grammar.NextPoint, pathNumber);

                case PathPart.NoHoleNoSpike:
                    return pathModel.NoHoleOrSpike.Create(grammar.NextPoint, pathNumber);

                case PathPart.AfterSweep:
                    return pathModel.AfterSweep.Create(grammar.NextPoint, pathNumber);

                case PathPart.AfterSpikeOrHole:
                    return pathModel.AfterSpikeOrHole.Create(grammar.NextPoint, pathNumber);

                case PathPart.PathSplitter:
                    return pathModel.PathSplitter.Create(grammar.NextPoint, pathNumber);

                case PathPart.RandomTripleAtLeastOne:
                    return pathModel.RandomTripletAtLeastOne.Create(grammar.NextPoint, pathNumber);

                case PathPart.LeftLog:
                    return pathModel.LogLeft.Create(grammar.NextPoint, pathNumber);

                case PathPart.Star:
                    return pathModel.Star.Create(grammar.NextPoint, pathNumber);

                case PathPart.SingleBlock:
                    return pathModel.SingleBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.Chaos:
                    return pathModel.Chaos.Create(grammar.NextPoint, pathNumber);

                case PathPart.Log:
                    return pathModel.Log.Create(grammar.NextPoint, pathNumber);

                case PathPart.LeftBlock:
                    return pathModel.LeftBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.RightBlock:
                    return pathModel.RightBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.LeftRightBlock:
                    return pathModel.LeftRightBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.LeftMiddleBlock:
                    return pathModel.LeftMiddleBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.RightMiddleBlock:
                    return pathModel.RightMiddleBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.AtLeastMiddleBlock:
                    return pathModel.AtLeastMiddleBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.AtLeastRightBlock:
                    return pathModel.AtLeastRightBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.AtLeastLeftBlock:
                    return pathModel.AtLeastLeftBlock.Create(grammar.NextPoint, pathNumber);

                case PathPart.RightLog:
                    return pathModel.LogRight.Create(grammar.NextPoint, pathNumber);

                case PathPart.RandomLog:
                    return pathModel.RandomLog.Create(grammar.NextPoint, pathNumber);

                default:
                    return pathModel.Chaos.Create(grammar.NextPoint, pathNumber);
            }
        }

        public void StopCreating()
        {
            _stopCreating = true;
        }

        private void CreatePath()
        {
            pathModel.TripleBlock.Create(start, pathNumber);
            _nextPoint = pathModel.BlockPart.Create(start + new Vector3(0, 0, 1), pathNumber);
        }

        public void Init(int initPathNumber, Vector3 startVec, int side)
        {
            pathNumber = initPathNumber;
            start = startVec;
            sidePath = side;
        }
    }
}