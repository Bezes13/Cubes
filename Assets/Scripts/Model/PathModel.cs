using System;
using System.Collections.Generic;
using NonTerminals;
using Terminals;
using UnityEngine;
using Random = System.Random;

namespace Model
{
    [CreateAssetMenu(fileName = "Model", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class PathModel : ScriptableObject
    {
        [SerializeField] private Cube cubePrefab;
        [SerializeField] private Pyramid pyramidPrefab;
        [SerializeField] private PathGenerator pathGenerator;
        [SerializeField] private PointsObject pointsObject;
    
        private Random _rnd;
        private int pathCount = 2;
        public List<PathGenerator> paths;

        // NonTerminals
        public Chaos Chaos;
        public HoleOrBlock HoleOrBlock;
        public BlockPart BlockPart;
        public AfterSweep AfterSweep;
        public JustTriplets JustTriplets;
        public AfterSpikeOrHole AfterSpikeOrHole;
        public LineOrChaos LineOrChaos;
        public NoHoleOrSpike NoHoleOrSpike;
        public PathSplitter PathSplitter;
    
        // Terminals
        public List<Terminal> Terminals;
        public Hole Hole;
        public RandomTripletAtLeastOne RandomTripletAtLeastOne;
        public LeftSweep LeftSweep;
        public RightSweep RightSweep;
        public SingleBlock SingleBlock;
        public SingleSpike SingleSpike;
        public TripleBlock TripleBlock;
        public UpStairs UpStairs;

        public enum Prefabtype
        {
            Cube,
            Pyramid
        }
    
        public void Init()
        {
            ResetModel();
            pathGenerator = FindObjectOfType<PathGenerator>();
            _rnd = new Random();
            paths = new List<PathGenerator> {new GameObject("Path").AddComponent<PathGenerator>()};
            paths[0].pathModel = this;
            paths[0].Init(0, new Vector3(0, 0.5f, 5), -1);
            Terminals = new List<Terminal>();
            RandomTripletAtLeastOne = new RandomTripletAtLeastOne(this, 0);
            Hole = new Hole(this);
            Chaos = new Chaos(this);
            AfterSweep = new AfterSweep(this);
            AfterSpikeOrHole = new AfterSpikeOrHole(this);
            NoHoleOrSpike = new NoHoleOrSpike(this);
            LineOrChaos = new LineOrChaos(this);
        
            JustTriplets = new JustTriplets(this);
            HoleOrBlock = new HoleOrBlock(this);
            BlockPart = new BlockPart(this);
            LeftSweep = new LeftSweep(this);
            RightSweep = new RightSweep(this);
            SingleBlock = new SingleBlock(this);
            SingleSpike = new SingleSpike(this);
            TripleBlock = new TripleBlock(this);
            UpStairs = new UpStairs(this);
            PathSplitter = new PathSplitter(this, pathGenerator);
        
            Terminals.Add(new LeftSweep(this));
            Terminals.Add(new RightSweep(this));
            Terminals.Add(new UpStairs(this));
            Terminals.Add(new SingleSpike(this));
            Terminals.Add(new SingleBlock(this));
            Terminals.Add(new TripleBlock(this));
            Terminals.Add(Hole);
            Terminals.Add(RandomTripletAtLeastOne);
        }

        public void CreateObject(Prefabtype prefabType, Vector3 position, int pathNumber, bool split = false)
        {
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
            var obj = Instantiate(cubePrefab, position, Quaternion.identity, parent);
            gen.cubes.Add(obj);
            obj.Init(_rnd.NextDouble(), pathNumber, gen, split);
        }

        public void CreateNewPath(Vector3 start, int sidePath)
        {
            var pathNumber = pathCount++;
            var path = new GameObject("Path"+ pathNumber).AddComponent<PathGenerator>();
            path.Init(pathNumber, start, sidePath);
            path.pathModel = this;
            paths.Add(path); 
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

        public void DestroyPath(int pathNumber)
        {
            var copyPaths = paths.GetRange(0, paths.Count);
            foreach (var generator in copyPaths)
            {
                if (generator.pathNumber == pathNumber)
                {
                    continue;
                }
                if (generator.sidePath == pathNumber)
                {
                    continue;
                }
                RemovePath(generator);
            }

            if (GetGeneratorByNumber(pathNumber).sidePath == -1)
            {
                return;
            }
            foreach (var generator in copyPaths)
            {
                if (generator.sidePath == pathNumber)
                {
                    RemovePath(generator);
                    return;
                }
            }
            return;
            Debug.Log("We are on " + pathNumber);
            // 0 -> -1
            // 147 -> 0
            // 148 -> 0
            // 149 -> 0
            // 150 -> 148
            // 151 -> 148
            // 152 -> 148
            // 153 -> 148
            // 154 -> 148
            var generatorByNumber = GetGeneratorByNumber(pathNumber);
            Debug.Log("The side path is " + generatorByNumber.sidePath);
            copyPaths = paths.GetRange(0, paths.Count);
            if (generatorByNumber.sidePath == -1)
            {
                foreach (PathGenerator generator in copyPaths)
                {
                    if (generator.sidePath == pathNumber)
                    {
                        var newPath = generator.pathNumber;
                        RemovePath(GetGeneratorByNumber(generator.pathNumber));
                        DeleteAllAssociatedPaths(newPath);
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("we just need to delete " + generatorByNumber.sidePath);
                RemovePath(GetGeneratorByNumber(generatorByNumber.sidePath));
                DeleteAllAssociatedPaths(generatorByNumber.sidePath);
                generatorByNumber.sidePath = -1;
            }
        }

        private void DeleteAllAssociatedPaths(int newPath)
        {
            var copyPaths = paths.GetRange(0, paths.Count);
            foreach (PathGenerator generator in copyPaths)
            {
                if (generator.sidePath == newPath)
                {
                    newPath = generator.pathNumber;
                    RemovePath(GetGeneratorByNumber(generator.pathNumber));
                    DeleteAllAssociatedPaths(newPath);
                }
            }
        }

        private void RemovePath(PathGenerator gen)
        {
            Debug.Log("So we delete " + gen.pathNumber);
            foreach (Cube child in gen.cubes)
            {
                child.Kill();
            }

            Destroy(gen.gameObject);
            paths.Remove(gen);
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
    }
}