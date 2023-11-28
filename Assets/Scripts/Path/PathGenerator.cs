using System;
using System.Collections.Generic;
using Model;
using Player;
using Signals;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Path
{
    public class PathGenerator : MonoBehaviour
    {
        public PathModel pathModel;
        public Vector3 start;
        private bool _stopCreating;
        private List<int> _activeLines;
        private List<(int, int)> _builtLines;
        private Camera _camera;
        private PlayerMovement _player;
        private const int ActiveLinesRange = 7;
        private const double StartBlockChance = 0.9;
        private double _blockChance = 0.9;
        private bool pause;

        public void Init(Vector3 startVec)
        {
            start = startVec;
            _camera = Camera.main;
            _player = FindObjectOfType<PlayerMovement>();
            for (int i = 0; i < ActiveLinesRange; i++)
            {
                var line = (int)start.x + (int)_player.transform.position.x + i;
                _activeLines.Add(line);
            }

            CreatePath();
        }

        private void Awake()
        {
            _activeLines = new List<int>();
            _builtLines = new List<(int, int)>();
            _player = FindObjectOfType<PlayerMovement>();
        }

        private void OnDestroy()
        {
            Supyrb.Signals.Get<StartGameSignal>().RemoveListener(CreatePath);
        }

        private void Update()
        {
            for (int i = 0; i < ActiveLinesRange; i++)
            {
                var line = (int)start.x + (int)_player.transform.position.x + i;
                if (!_activeLines.Contains(line))
                {
                    BuildLine(line);
                }
            }

            _activeLines.Clear();
            for (int i = 0; i < ActiveLinesRange; i++)
            {
                var line = (int)start.x + (int)_player.transform.position.x + i;
                _activeLines.Add(line);
            }

            ContinuePath();
        }

        private void FixedUpdate()
        {
            _blockChance = StartBlockChance - Math.Min(0.7, start.z * 0.01);
        }

        private void ContinuePath()
        {
            if (_stopCreating)
            {
                return;
            }

            var screenPoint = _camera.WorldToViewportPoint(start);
            var onScreen = screenPoint.y > 0 && screenPoint.y < 0.8;
            if (!onScreen)
            {
                return;
            }

            start += Vector3.forward;
            CreatePath();
        }

        private void CreatePath()
        {
            _builtLines.RemoveAll(tuple => _activeLines.Contains(tuple.Item1));
            foreach (var line in _activeLines)
            {
                CreateAtPos(new Vector3(line, start.y, start.z));
                _builtLines.Add((line, (int)start.z + 1));
            }
        }

        private void BuildLine(int line)
        {
            var startLine = (int)_player.PlayerPos().z;
            var buildLine = _builtLines.FindAll(tuple => tuple.Item1 == line);
            if (buildLine.Count == 1)
            {
                startLine = buildLine[0].Item2;
            }

            var end = (int)start.z + 1;
            for (var i = startLine; i < end; i++)
            {
                CreateAtPos(new Vector3(line, this.start.y, i));
            }

            _builtLines.Add((line, end + 1));
        }

        private void CreateAtPos(Vector3 position)
        {
            if (!(Random.value <= _blockChance)) return;
            pathModel.CreatePathObject(PathModel.PrefabType.Cube, position);
            if (!(Random.value <= 0.05)) return;
            pathModel.CreatePathObject(PathModel.PrefabType.Cube, position + Vector3.up);
        }
    }
}