using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Player;
using Signals;
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
        private List<(int, int)> _buildedLines;
        private Camera _camera;
        private PlayerMovement _player;
        private const int ActiveLinesRange = 7;

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
            _buildedLines = new List<(int, int)>();
            _player = FindObjectOfType<PlayerMovement>();
            //Supyrb.Signals.Get<StartGameSignal>().AddListener(CreatePath);
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
            _buildedLines.RemoveAll(tuple => _activeLines.Contains(tuple.Item1));
            foreach (var line in _activeLines)
            {
                CreateAtPos(new Vector3(line, start.y, start.z));
                _buildedLines.Add((line, (int) start.z + 1));
            }
        }

        private void BuildLine(int line)
        {
            var start = (int)_player.PlayerPos().z;
            var buildLine = _buildedLines.FindAll(tuple => tuple.Item1 == line);
            if (buildLine.Count == 1)
            {
                start = buildLine[0].Item2;
            }

            var end = (int)this.start.z + 1;
            for (int i = start; i < end; i++)
            {
                CreateAtPos(new Vector3(line, this.start.y, i));
            }

            _buildedLines.Add((line, end + 1));
        }

        private void CreateAtPos(Vector3 position)
        {
            print(this.gameObject.GetInstanceID());
            print(position.z);
            pathModel.CreatePathObject(PathModel.PrefabType.Cube, position);
            if (Random.value <= 0.3)
            {
                pathModel.CreatePathObject(PathModel.PrefabType.Cube, position + Vector3.up);
                if (Random.value <= 0.1)
                {
                    pathModel.CreatePathObject(PathModel.PrefabType.Coin, position + Vector3.up*2);
                }
            }
            if (Random.value >= 0.9)
            {
                pathModel.CreatePathObject(PathModel.PrefabType.Coin, position + Vector3.up);
            }
        }
    }
}