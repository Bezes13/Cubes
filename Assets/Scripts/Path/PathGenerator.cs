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
        private List<(int, int)> _builtLines;
        private List<PathPart> _pathParts;
        private Camera _camera;
        private PlayerMovement _player;
        private const int ActiveLinesRange = 7;
        private double _difficulty;
        private bool pause;

        private enum PathPart
        {
            ClassicEasy, // Underground with 2 Blocks on Top
            ClassicMiddle, // Underground with Big Blocks and normal Blocks
            ClassicHard, // Underground with Bigblocks
            BridgeEasy, // Just the Block in the middle
            BridgeMiddle,
            BridgeHard,
            JumpesEasy,
            JumpesMidlle,
            JumpesHard,
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
            _builtLines = new List<(int, int)>();
            _pathParts = new List<PathPart>();
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
        }

        private void HardPath(float value)
        {
            if (value >= 0.66 && _pathParts[_pathParts.Count-1] != PathPart.ClassicHard)
            {
                print("ClassicHard");
                AddToPath(PathPart.ClassicHard,10);
                return;
            }
            if (value >= 0.33 && _pathParts[_pathParts.Count-1] != PathPart.JumpesHard)
            {
                print("JumpHard");
                AddToPath(PathPart.JumpesHard,10);
                return;
            }
            print("BridgeHard");
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
                print("ClassicMiddle");
                AddToPath(PathPart.ClassicMiddle,10);
                return;
            }
            if (value >= 0.33)
            {
                print("JumpesMiddle");
                AddToPath(PathPart.JumpesMidlle,10);
                return;
            }
            print("BridgeMiddle");
            AddToPath(PathPart.BridgeEasy,3);
            AddToPath(PathPart.BridgeMiddle,1);
            AddToPath(PathPart.BridgeEasy,3);
            AddToPath(PathPart.BridgeMiddle,1);
            AddToPath(PathPart.BridgeEasy,2);
        }

        private void EasyPath(float value)
        {
            if (value >= 0.66)
            {
                print("ClassicEasy");
                AddToPath(PathPart.ClassicEasy,10);
                return;
            }
            if (value >= 0.33)
            {
                print("JumpesEasy");
                AddToPath(PathPart.JumpesEasy,10);
                return;
            }

            if (_pathParts.Count > 0 && _pathParts[_pathParts.Count - 1] != PathPart.BridgeEasy)
            {
                print("BrdigeEasy");
                AddToPath(PathPart.BridgeEasy,5);    
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
            // Remove the activeLines from the buildedLines to add the updated one 
            _builtLines.RemoveAll(tuple => _activeLines.Contains(tuple.Item1));
            foreach (var line in _activeLines)
            {
                _builtLines.Add((line, (int)start.z + 1));
            }
            
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
                    break;
                case PathPart.JumpesEasy:
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
                case PathPart.JumpesMidlle:
                    pos1 = GetRightLine(Random.value, _activeLines);
                    var pos2 = GetRightLine(Random.value, _activeLines);
                    while (pos1 == pos2)
                    {
                        pos2 = GetRightLine(Random.value, _activeLines);
                    }
                    pathModel.CreatePathObject(PathModel.PrefabType.Cube,
                        new Vector3(_activeLines[pos1], start.y, start.z));
                    pathModel.CreatePathObject(PathModel.PrefabType.BigCube,
                        new Vector3(_activeLines[pos2], start.y, start.z));
                    break;
                case PathPart.JumpesHard:
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
            pathModel.CreatePathObject(PathModel.PrefabType.Cube, position);
        }
    }
}