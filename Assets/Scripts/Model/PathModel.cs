using System.Collections.Generic;
using NonTerminals;
using Objects;
using Path;
using Terminals;
using UnityEngine;
using Random = System.Random;

namespace Model
{
    /// <summary>
    /// Scriptable Object which contains all informations about the existing paths.
    /// Contains the functionality to Create and delete paths as well as creating Single Objects.
    /// </summary>
    [CreateAssetMenu(fileName = "Model", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class PathModel : ScriptableObject
    {
        private static readonly Vector3 StartVec = new Vector3(0, 0.5f, 5);

        [SerializeField] private PathObject pathObjectPrefab;
        [SerializeField] private PathObject pathObjectPrefab2;
        [SerializeField] private PathObject pyramidPrefab;
        [SerializeField] private PathObject logPrefab;
        [SerializeField] private PathObject starPrefab;

        // NonTerminals
        public Chaos Chaos;
        public AfterSweep AfterSweep;
        public AfterSpikeOrHole AfterSpikeOrHole;
        public NoHoleOrSpike NoHoleOrSpike;
        public PathSplitter PathSplitter;
        public AfterStairs AfterStairs;
        public AtLeastLeftBlock AtLeastLeftBlock;
        public AtLeastMiddleBlock AtLeastMiddleBlock;
        public AtLeastRightBlock AtLeastRightBlock;
        public RandomLog RandomLog;
        public LastLeftOne LastLeftOne;
        public LastMidOne LastMidOne;
        public LastRightOne LastRightOne;

        // Terminals
        public Hole Hole;
        public RandomTripletAtLeastOne RandomTripletAtLeastOne;
        public LeftSweep LeftSweep;
        public RightSweep RightSweep;
        public SingleBlock SingleBlock;
        public SingleSpike SingleSpike;
        public TripleBlock TripleBlock;
        public UpStairs UpStairs;
        public Star Star;
        public Log Log;
        public LogLeft LogLeft;
        public LogRight LogRight;
        public LeftBlock LeftBlock;
        public RightBlock RightBlock;
        public LeftMiddleBlock LeftMiddleBlock;
        public LeftRightBlock LeftRightBlock;
        public RightMiddleBlock RightMiddleBlock;

        private Random _rnd;
        private List<PathGenerator> _paths;
        private int _pathCount = 2;

        public enum PrefabType
        {
            Cube,
            Cube2,
            Log,
            Pyramid,
            Star
        }

        public void Init()
        {
            ResetModel();
            _rnd = new Random();
            _paths = new List<PathGenerator> {new GameObject("Path").AddComponent<PathGenerator>()};
            _paths[0].pathModel = this;
            _paths[0].Init(0, StartVec, -1);

            //Terminals
            RandomTripletAtLeastOne = new RandomTripletAtLeastOne(this);
            Hole = new Hole(this);
            LeftSweep = new LeftSweep(this);
            RightSweep = new RightSweep(this);
            SingleBlock = new SingleBlock(this);
            SingleSpike = new SingleSpike(this);
            TripleBlock = new TripleBlock(this);
            UpStairs = new UpStairs(this);
            Log = new Log(this);
            LogLeft = new LogLeft(this);
            LogRight = new LogRight(this);
            Star = new Star(this);
            LeftBlock = new LeftBlock(this);
            RightBlock = new RightBlock(this);
            LeftRightBlock = new LeftRightBlock(this);
            LeftMiddleBlock = new LeftMiddleBlock(this);
            RightMiddleBlock = new RightMiddleBlock(this);

            //NonTerminals
            Chaos = new Chaos(this);
            AfterSweep = new AfterSweep(this);
            AfterSpikeOrHole = new AfterSpikeOrHole(this);
            NoHoleOrSpike = new NoHoleOrSpike(this);
            PathSplitter = new PathSplitter(this);
            AfterStairs = new AfterStairs(this);
            AtLeastMiddleBlock = new AtLeastMiddleBlock(this);
            AtLeastRightBlock = new AtLeastRightBlock(this);
            AtLeastLeftBlock = new AtLeastLeftBlock(this);
            RandomLog = new RandomLog(this);
            LastLeftOne = new LastLeftOne(this);
            LastMidOne = new LastMidOne(this);
            LastRightOne = new LastRightOne(this);
        }

        public void CreatePathObject(PrefabType prefabType, Vector3 position, int pathNumber, bool split = false)
        {
            foreach (var generator in _paths)
            {
                foreach (var cube in generator.cubes)
                {
                    if (Vector3.Distance(position, cube.pos) < 0.1)
                    {
                        return;
                    }
                }
            }

            var gen = GetGeneratorByNumber(pathNumber);
            if (gen == null)
            {
                return;
            }

            var prefab = prefabType.Equals(PrefabType.Pyramid) ? pyramidPrefab :
                prefabType.Equals(PrefabType.Star) ? starPrefab :
                prefabType.Equals(PrefabType.Log) ? logPrefab :
                gen.type == PrefabType.Cube ? pathObjectPrefab : pathObjectPrefab2;
            var parent = gen.transform;

            var obj = Instantiate(prefab, position, Quaternion.identity, parent);
            gen.cubes.Add(obj);
            obj.Init(_rnd.NextDouble(), pathNumber, gen, split);
        }

        public int CreateNewPath(Vector3 start, int sidePath)
        {
            var pathNumber = _pathCount++;
            var path = new GameObject("Path-" + pathNumber).AddComponent<PathGenerator>();
            path.Init(pathNumber, start, sidePath);
            path.type = PrefabType.Cube2;
            path.pathModel = this;
            _paths.Add(path);
            return pathNumber;
        }

        public void DestroyPath(int pathNumberToKeep, Vector3 playerPos)
        {
            var toKeep = PathGeneratorsToKeep(pathNumberToKeep, playerPos.z);
            var copyPaths = _paths.GetRange(0, _paths.Count);
            foreach (var generator in copyPaths)
            {
                if (toKeep.Contains(generator))
                {
                    continue;
                }

                RemovePath(generator);
            }
        }

        public void IncreaseDifficulty(float value)
        {
            Chaos.IncreaseDifficult(value);
            AfterSweep.IncreaseDifficult(value);
            AfterSpikeOrHole.IncreaseDifficult(value);
            NoHoleOrSpike.IncreaseDifficult(value);
            AfterStairs.IncreaseDifficult(value);
            AtLeastMiddleBlock.IncreaseDifficult(value);
            AtLeastRightBlock.IncreaseDifficult(value);
            AtLeastLeftBlock.IncreaseDifficult(value);
        }

        public PathGenerator GetGeneratorByNumber(int number)
        {
            foreach (PathGenerator generator in _paths)
            {
                if (generator.pathNumber.Equals(number))
                {
                    return generator;
                }
            }

            return null;
        }

        private List<PathGenerator> PathGeneratorsToKeep(int pathNumber, float playerPos)
        {
            var toKeep = new List<PathGenerator>();
            var generatorByNumber = GetGeneratorByNumber(pathNumber);
            toKeep.Add(generatorByNumber);
            if (generatorByNumber.sidePath == -1 || GetGeneratorByNumber(generatorByNumber.sidePath) == null)
            {
                foreach (var generator in _paths)
                {
                    if (generator.sidePath == pathNumber)
                    {
                        if (generator.start.z - 1 < playerPos)
                        {
                            continue;
                        }

                        toKeep.Add(generator);
                        AddFurtherPathsToKeep(generator.pathNumber, toKeep);
                    }
                }
            }
            else
            {
                AddFurtherPathsToKeep(pathNumber, toKeep);
            }

            return toKeep;
        }

        private void AddFurtherPathsToKeep(int generatorPathNumber, List<PathGenerator> toKeep)
        {
            foreach (var generator in _paths)
            {
                if (generator.sidePath == generatorPathNumber)
                {
                    toKeep.Add(generator);
                    AddFurtherPathsToKeep(generator.pathNumber, toKeep);
                }
            }
        }

        private void RemovePath(PathGenerator gen)
        {
            foreach (PathObject child in gen.cubes)
            {
                child.Kill();
            }

            Destroy(gen.gameObject, 2f);
            gen.StopCreating();
            _paths.Remove(gen);
        }

        private void ResetModel()
        {
            if (_paths == null)
            {
                return;
            }

            foreach (var path in _paths)
            {
                if (path != null)
                {
                    Destroy(path.gameObject);
                }
            }

            _paths.Clear();
        }
    }
}