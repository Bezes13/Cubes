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
        public PathModel.PrefabType type;
        public List<PathObject> cubes;
        public Vector3 start;
        public int pathNumber;
        public int sidePath;

        private bool _stopCreating;

        private Camera _camera;

        private Grammar _nextPoint;

        public void Init(int initPathNumber, Vector3 startVec, int side)
        {
            pathNumber = initPathNumber;
            start = startVec;
            sidePath = side;
        }

        public void CreateInBetween(Grammar grammar)
        {
            SwitchCase(grammar);
        }

        public void StopCreating()
        {
            _stopCreating = true;
        }

        private void Awake()
        {
            cubes = new List<PathObject>();
            Supyrb.Signals.Get<StartGameSignal>().AddListener(CreatePath);
        }

        private void Start()
        {
            _camera = Camera.main;
            CreatePath();
        }

        private void OnDestroy()
        {
            Supyrb.Signals.Get<StartGameSignal>().RemoveListener(CreatePath);
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

        private Grammar SwitchCase(Grammar grammar)
        {
            return grammar.Part switch
            {
                PathPart.RightSweep => pathModel.RightSweep.Create(grammar.NextPoint, pathNumber),
                PathPart.LeftSweep => pathModel.LeftSweep.Create(grammar.NextPoint, pathNumber),
                PathPart.Hole => pathModel.Hole.Create(grammar.NextPoint, pathNumber),
                PathPart.SingleSpike => pathModel.SingleSpike.Create(grammar.NextPoint, pathNumber),
                PathPart.TripleBlock => pathModel.TripleBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.UpStairs => pathModel.UpStairs.Create(grammar.NextPoint, pathNumber),
                PathPart.AfterStairs => pathModel.AfterStairs.Create(grammar.NextPoint, pathNumber),
                PathPart.NoHoleNoSpike => pathModel.NoHoleOrSpike.Create(grammar.NextPoint, pathNumber),
                PathPart.AfterSweep => pathModel.AfterSweep.Create(grammar.NextPoint, pathNumber),
                PathPart.AfterSpikeOrHole => pathModel.AfterSpikeOrHole.Create(grammar.NextPoint, pathNumber),
                PathPart.PathSplitter => pathModel.PathSplitter.Create(grammar.NextPoint, pathNumber),
                PathPart.RandomTripleAtLeastOne => pathModel.RandomTripletAtLeastOne.Create(grammar.NextPoint, pathNumber),
                PathPart.LeftLog => pathModel.LogLeft.Create(grammar.NextPoint, pathNumber),
                PathPart.Star => pathModel.Star.Create(grammar.NextPoint, pathNumber),
                PathPart.SingleBlock => pathModel.SingleBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.Chaos => pathModel.Chaos.Create(grammar.NextPoint, pathNumber),
                PathPart.Log => pathModel.Log.Create(grammar.NextPoint, pathNumber),
                PathPart.LeftBlock => pathModel.LeftBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.RightBlock => pathModel.RightBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.LeftRightBlock => pathModel.LeftRightBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.LeftMiddleBlock => pathModel.LeftMiddleBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.RightMiddleBlock => pathModel.RightMiddleBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.AtLeastMiddleBlock => pathModel.AtLeastMiddleBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.AtLeastRightBlock => pathModel.AtLeastRightBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.AtLeastLeftBlock => pathModel.AtLeastLeftBlock.Create(grammar.NextPoint, pathNumber),
                PathPart.RightLog => pathModel.LogRight.Create(grammar.NextPoint, pathNumber),
                PathPart.RandomLog => pathModel.RandomLog.Create(grammar.NextPoint, pathNumber),
                PathPart.LastRightOne => pathModel.LastRightOne.Create(grammar.NextPoint, pathNumber),
                PathPart.LastLeftOne => pathModel.LastLeftOne.Create(grammar.NextPoint, pathNumber),
                PathPart.LastMiddleOne => pathModel.LastMidOne.Create(grammar.NextPoint, pathNumber),
                _ => pathModel.Chaos.Create(grammar.NextPoint, pathNumber)
            };
        }

        private void CreatePath()
        {
            pathModel.TripleBlock.Create(start, pathNumber);
            _nextPoint = pathModel.Chaos.Create(start + new Vector3(0, 0, 1), pathNumber);
        }
    }
}