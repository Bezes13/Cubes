using System;
using System.Collections.Generic;
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
        private List<PathPart> _pathParts;
        private Camera _camera;
        private PlayerMovement _player;
        private const int ActiveLinesRange = 5;
        private double _difficulty;
        private bool _pause;

        private enum PathPart
        {
            ClassicEasy, // Underground with 2 Blocks on Top
            ClassicMiddle, // Underground with Big Blocks and normal Blocks
            ClassicHard, // Underground with BigBlocks
            BridgeEasy, // Just the Block in the middle
            BridgeMiddle,
            BridgeHard,
            JumpEasy,
            JumpMedium,
            JumpHard,
            Empty
        }

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
            ExtendPathPlan();
            CreatePath();
        }

        private void Awake()
        {
            _activeLines = new List<int>();
            _pathParts = new List<PathPart>();
            _player = FindObjectOfType<PlayerMovement>();
        }

        private void OnDestroy()
        {
            Supyrb.Signals.Get<StartGameSignal>().RemoveListener(CreatePath);
        }

        private void Update()
        {
            ContinuePath();
        }

        private void FixedUpdate()
        {
            _difficulty = Math.Min(1, start.z * 0.01);
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
            if (start.z > _pathParts.Count)
            {
                ExtendPathPlan();
            }
            CreatePath();
        }

        private void ExtendPathPlan()
        {
            if (_difficulty >= 0.66)
            {
                HardPath(Random.value);
                return;
            }
            if (_difficulty >= 0.33)
            {
                MediumPath(Random.value);
                return;
            }
            EasyPath(Random.value);
            AddToPath(PathPart.Empty,1);
        }

        private void HardPath(float value)
        {
            if (value >= 0.66 && _pathParts[_pathParts.Count-1] != PathPart.ClassicHard)
            {
                AddToPath(PathPart.ClassicHard,10);
                return;
            }
            if (value >= 0.33 && _pathParts[_pathParts.Count-1] != PathPart.JumpHard)
            {
                AddToPath(PathPart.JumpHard,10);
                return;
            }
            AddToPath(PathPart.BridgeEasy,3);
            AddToPath(PathPart.BridgeHard,1);
            AddToPath(PathPart.BridgeEasy,3);
            AddToPath(PathPart.BridgeHard,1);
            AddToPath(PathPart.BridgeEasy,2);
        }

        private void MediumPath(float value)
        {
            if (value >= 0.66)
            {
                AddToPath(PathPart.ClassicMiddle,10);
                return;
            }
            if (value >= 0.33)
            {
                AddToPath(PathPart.JumpMedium,10);
                return;
            }
            AddToPath(PathPart.BridgeEasy,2);
            AddToPath(PathPart.BridgeMiddle,1);
            AddToPath(PathPart.BridgeEasy,2);
            AddToPath(PathPart.BridgeMiddle,1);
            AddToPath(PathPart.BridgeEasy,2);
        }

        private void EasyPath(float value)
        {
            if (value >= 0.66)
            {
                AddToPath(PathPart.ClassicEasy,10);
                return;
            }
            if (value >= 0.33)
            {
                AddToPath(PathPart.JumpEasy,10);
                return;
            }

            if (_pathParts.Count > 0 && _pathParts[_pathParts.Count - 1] != PathPart.BridgeEasy)
            {
                AddToPath(PathPart.BridgeEasy,2);    
            }
            else
            {
                EasyPath(Random.value);
            }
            
        }

        private void AddToPath(PathPart part, int times)
        {
            for (int i = 0; i < times; i++)
            {
                _pathParts.Add(part);
            }
        }

        private void CreatePath()
        {
            switch (_pathParts[(int)start.z - 5])
            {
                case PathPart.ClassicEasy:
                    double random = Random.value;
                    var i = 1;
                    foreach (var line in _activeLines)
                    {
                        CreateAtPos(new Vector3(line, start.y, start.z));
                        if ((double)i / _activeLines.Count > random && (double)(i - 1) / _activeLines.Count < random)
                        {
                            CreateAtPos(new Vector3(line, start.y + 1, start.z));
                        }

                        i++;
                    }

                    break;
                case PathPart.ClassicMiddle:
                    var pos1 = GetRightLine(Random.value, _activeLines);
                    var bigOne = GetRightLine(Random.value, _activeLines);
                    while (pos1 == bigOne)
                    {
                        bigOne = GetRightLine(Random.value, _activeLines);
                    }

                    foreach (var line in _activeLines)
                    {
                        CreateAtPos(new Vector3(line, start.y, start.z));
                    }

                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[pos1], start.y + 1, start.z));
                    pathModel.CreatePathObject(PathModel.PrefabType.BigCube,
                        new Vector3(_activeLines[bigOne], start.y + 1, start.z));
                    break;
                case PathPart.ClassicHard:
                    pos1 = GetRightLine(Random.value, _activeLines);
                    bigOne = GetRightLine(Random.value, _activeLines);
                    while (pos1 == bigOne)
                    {
                        bigOne = GetRightLine(Random.value, _activeLines);
                    }

                    foreach (var line in _activeLines)
                    {
                        CreateAtPos(new Vector3(line, start.y, start.z));
                    }

                    pathModel.CreatePathObject(PathModel.PrefabType.BigCube,
                        new Vector3(_activeLines[bigOne], start.y + 1, start.z));
                    pathModel.CreatePathObject(PathModel.PrefabType.BigCube,
                        new Vector3(_activeLines[bigOne], start.y + 1, start.z));
                    break;
                case PathPart.BridgeEasy:
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[_activeLines.Count / 2], start.y, start.z));
                    break;
                case PathPart.BridgeMiddle:
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[_activeLines.Count / 2], start.y, start.z));
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[_activeLines.Count / 2], start.y + 1, start.z));
                    break;
                case PathPart.BridgeHard:
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[_activeLines.Count / 2], start.y, start.z));
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[_activeLines.Count / 2], start.y + 1, start.z));
                    pathModel.CreatePathObject(PathModel.PrefabType.BigCube,
                        new Vector3(_activeLines[_activeLines.Count / 2], start.y + 2, start.z));
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[_activeLines.Count / 2 + 1], start.y, start.z));
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[_activeLines.Count / 2 - 1], start.y, start.z));
                    break;
                case PathPart.Empty:
                    foreach (var line in _activeLines)
                    {
                        pathModel.CreatePathObject(PathModel.PrefabType.Coin, new Vector3(line, start.y + 2.5f, start.z));
                    }
                    break;
                case PathPart.JumpEasy:
                    pos1 = GetRightLine(Random.value, _activeLines);
                    bigOne = GetRightLine(Random.value, _activeLines);
                    while (pos1 == bigOne)
                    {
                        bigOne = GetRightLine(Random.value, _activeLines);
                    }

                    for (var index = 0; index < _activeLines.Count; index++)
                    {
                        if(index == pos1 || index == bigOne)continue;
                        var line = _activeLines[index];
                        CreateAtPos(new Vector3(line, start.y, start.z));
                    }

                    break;
                case PathPart.JumpMedium:
                    pos1 = GetRightLine(Random.value, _activeLines);
                    var pos2 = GetRightLine(Random.value, _activeLines);
                    while (pos1 == pos2)
                    {
                        pos2 = GetRightLine(Random.value, _activeLines);
                    }
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[pos1], start.y, start.z));
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[pos2], start.y, start.z));
                    break;
                case PathPart.JumpHard:
                    pos1 = GetRightLine(Random.value, _activeLines);
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[pos1], start.y, start.z));
                    break;
            }
        }

        private static int GetRightLine<T>(double value, List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((double)(i + 1) / list.Count > value && (double)i / list.Count < value)
                {
                    return i;
                }
            }

            throw new Exception();
        }

        private void CreateAtPos(Vector3 position)
        {
            pathModel.CreatePathObject(PathModel.PrefabType.Cube, position);
        }
    }
}