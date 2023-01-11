using System.Collections.Generic;
using NonTerminals;
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
        [SerializeField] private Pyramid pyramidPrefab;

        public List<PathGenerator> paths;

        // NonTerminals
        public Chaos Chaos;
        public HoleOrBlock HoleOrBlock;
        public BlockPart BlockPart;
        public AfterSweep AfterSweep;
        public AfterSpikeOrHole AfterSpikeOrHole;
        public NoHoleOrSpike NoHoleOrSpike;
        public PathSplitter PathSplitter;
    
        // Terminals
        public Hole Hole;
        public RandomTripletAtLeastOne RandomTripletAtLeastOne;
        public LeftSweep LeftSweep;
        public RightSweep RightSweep;
        public SingleBlock SingleBlock;
        public SingleSpike SingleSpike;
        public TripleBlock TripleBlock;
        public UpStairs UpStairs;
        
        private Random _rnd;
        private int _pathCount = 2;

// TODO unify CUbe and pyramid
// TODO get new pyramid asset
// TODO show path vanish correct
// TODO make path drop more beatiful
// TODO improve rules
        public enum Prefabtype
        {
            Cube,
            Cube2,
            Pyramid
        }
    
        public void Init()
        {
            ResetModel();
            _rnd = new Random();
            paths = new List<PathGenerator> {new GameObject("Path").AddComponent<PathGenerator>()};
            paths[0].pathModel = this;
            paths[0].Init(0, new Vector3(0, 0.5f, 5), -1);
            
            //Terminals
            RandomTripletAtLeastOne = new RandomTripletAtLeastOne(this, 0);
            Hole = new Hole(this);
            HoleOrBlock = new HoleOrBlock(this);
            LeftSweep = new LeftSweep(this);
            RightSweep = new RightSweep(this);
            SingleBlock = new SingleBlock(this);
            SingleSpike = new SingleSpike(this);
            TripleBlock = new TripleBlock(this);
            UpStairs = new UpStairs(this);
            
            //NonTerminals
            Chaos = new Chaos(this);
            AfterSweep = new AfterSweep(this);
            AfterSpikeOrHole = new AfterSpikeOrHole(this);
            NoHoleOrSpike = new NoHoleOrSpike(this);
            BlockPart = new BlockPart(this);
            PathSplitter = new PathSplitter(this);
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
            var parent = gen.transform;
            if (prefabType.Equals(Prefabtype.Pyramid))
            {
                var pyramid = Instantiate(pyramidPrefab, position, Quaternion.Euler(0,0,0), parent);
                pyramid.Init(_rnd.NextDouble(), split);
                return;
            }
            var obj = Instantiate(gen.type == Prefabtype.Cube ? cubePrefab:cubePrefab2, position, Quaternion.identity, parent);
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

        public List<PathGenerator> PathGeneratorsToKeep(int pathNumber, float playerPos)
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
            Debug.Log("So we delete " + gen.pathNumber);
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