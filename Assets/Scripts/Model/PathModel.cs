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
        [SerializeField] private Cube cubePrefab;
        [SerializeField] private Cube cubePrefab2;
        [SerializeField] private Cube pyramidPrefab;
        [SerializeField] private Cube cubeOnTop;
        [SerializeField] private Cube starPrefab;

        public List<PathGenerator> paths;

        // NonTerminals
        public Chaos Chaos;
        public HoleOrBlock HoleOrBlock;
        public BlockPart BlockPart;
        public AfterSweep AfterSweep;
        public AfterSpikeOrHole AfterSpikeOrHole;
        public NoHoleOrSpike NoHoleOrSpike;
        public PathSplitter PathSplitter;
        public AfterStairs AfterStairs;
        public AtLeastLeftBlock AtLeastLeftBlock;
        public AtLeastMiddleBlock AtLeastMiddleBlock;
        public AtLeastRightBlock AtLeastRightBlock;
        public RandomLog RandomLog;
        
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
        private int _pathCount = 2;

// todo make vanish warning correct
// TODO improve rules
        public enum Prefabtype
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
            paths = new List<PathGenerator> {new GameObject("Path").AddComponent<PathGenerator>()};
            paths[0].pathModel = this;
            paths[0].Init(0, new Vector3(0, 0.5f, 5), -1);
            
            //Terminals
            RandomTripletAtLeastOne = new RandomTripletAtLeastOne(this);
            Hole = new Hole(this);
            HoleOrBlock = new HoleOrBlock(this);
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
            BlockPart = new BlockPart(this);
            PathSplitter = new PathSplitter(this);
            AfterStairs = new AfterStairs(this);
            AtLeastMiddleBlock = new AtLeastMiddleBlock(this);
            AtLeastRightBlock = new AtLeastRightBlock(this);
            AtLeastLeftBlock = new AtLeastLeftBlock(this);
            RandomLog = new RandomLog(this);
        }

        public void CreateObject(Prefabtype prefabType, Vector3 position, int pathNumber, bool split = false)
        {
            foreach (var generator in paths)
            {
                foreach (Cube cube in generator.cubes)
                {
                    if (Vector3.Distance(position, cube.pos)<0.1)
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
            Cube prefab = prefabType.Equals(Prefabtype.Pyramid) ? pyramidPrefab : prefabType.Equals(Prefabtype.Star) ? starPrefab : prefabType.Equals(Prefabtype.Log) ? cubeOnTop : gen.type == Prefabtype.Cube ? cubePrefab : cubePrefab2;
            var parent = gen.transform;
            
            var obj = Instantiate(prefab, position, Quaternion.identity, parent);
            gen.cubes.Add(obj);
            obj.Init(_rnd.NextDouble(), pathNumber, gen, split);
        }

        public int CreateNewPath(Vector3 start, int sidePath)
        {
            var pathNumber = _pathCount++;
            var path = new GameObject("Path"+ pathNumber).AddComponent<PathGenerator>();
            path.Init(pathNumber, start, sidePath);
            path.type = Prefabtype.Cube2;
            path.pathModel = this;
            paths.Add(path);
            return pathNumber;
        }

        public void DestroyPath(int pathNumberToKeep, Vector3 playerPos)
        {
            var toKeep = PathGeneratorsToKeep(pathNumberToKeep, playerPos.z);
            var copyPaths = paths.GetRange(0, paths.Count);
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
            BlockPart.IncreaseDifficult(value);
            PathSplitter.IncreaseDifficult(value);
            AfterStairs.IncreaseDifficult(value);
            AtLeastMiddleBlock.IncreaseDifficult(value);
            AtLeastRightBlock.IncreaseDifficult(value);
            AtLeastLeftBlock.IncreaseDifficult(value);
        }

        private List<PathGenerator> PathGeneratorsToKeep(int pathNumber, float playerPos)
        {
            List<PathGenerator> toKeep = new List<PathGenerator>();
            var generatorByNumber = GetGeneratorByNumber(pathNumber);
            toKeep.Add(generatorByNumber);
            if (generatorByNumber.sidePath == -1 || GetGeneratorByNumber(generatorByNumber.sidePath) == null)
            {
                foreach (var generator in paths)
                {

                    if (generator.sidePath == pathNumber)
                    {
                        if (generator.start.z - 1 < playerPos)
                        {
                            continue;
                        }
                        toKeep.Add(generator);
                        AddFurtherPaths(generator.pathNumber, toKeep);
                    }
                }
            }
            else
            {
                AddFurtherPaths(pathNumber, toKeep);
            }

            return toKeep;
        }

        private void AddFurtherPaths(int generatorPathNumber, List<PathGenerator> toKeep)
        {
            foreach (var generator in paths)
            {
                if (generator.sidePath == generatorPathNumber)
                {
                    toKeep.Add(generator);
                    AddFurtherPaths(generator.pathNumber, toKeep);
                }
            }
        }

        public PathGenerator GetGeneratorByNumber(int number)
        {
            foreach (PathGenerator generator in paths)
            {
                if (generator.pathNumber.Equals(number))
                {
                    return generator;
                }
            }

            return null;
        }

        private void RemovePath(PathGenerator gen)
        {
            foreach (Cube child in gen.cubes)
            {
                child.Kill();
            }

            Destroy(gen.gameObject, 2f);
            gen.StopCreating();
            paths.Remove(gen);
        }

        private void ResetModel()
        {
            if (paths == null)
            {
                return;
            }
            foreach (var path in paths)
            {
                if (path != null)
                {
                    Destroy(path.gameObject);   
                }
            }
            paths.Clear();
        }
    }
}