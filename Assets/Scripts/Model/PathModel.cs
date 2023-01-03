using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using NonTerminals;
using Terminals;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "Model", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class PathModel : ScriptableObject
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Pyramid pyramidPrefab;
    [SerializeField] private PathGenerator pathGenerator;
    
    private Random _rnd;
    public GameObject path;
    public GameObject secondPath;
    
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
        pathGenerator = FindObjectOfType<PathGenerator>();
        _rnd = new Random();
        path  = new GameObject("Path");
        secondPath  = new GameObject("Path2");
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
    
    public Terminal AllButNotOne(Type notIncluded)
    {
        Terminal terminal = Terminals[UnityEngine.Random.Range(0, Terminals.Count - 1)];
        while (terminal.GetType() == notIncluded)
        {
            terminal = Terminals[UnityEngine.Random.Range(0, Terminals.Count - 1)];
        }

        return terminal;
    }

    public void CreateObject(Prefabtype prefabType, Vector3 position, int pathNumber)
    {
        var parent = pathNumber == 0 ? path.transform : secondPath.transform;
        if (prefabType.Equals(Prefabtype.Pyramid))
        {
            var pyramid = Instantiate(pyramidPrefab, position, Quaternion.Euler(0,0,0), parent);
            pyramid.Init(_rnd.NextDouble());
            return;
        }
        var obj = Instantiate(cubePrefab, position, Quaternion.identity, parent);
            
        obj.Init(_rnd.NextDouble());
    }

    private const int MaxElements = 100;

    public void ResetModel()
    {
        foreach (Transform child in path.transform) {
            Destroy(child.gameObject);
        }
    }

    public void DestroyPath()
    {
        foreach (Transform child in path.transform) {
            child.GetComponent<Cube>().DieHard();
            child.GetComponent<Pyramid>().DieHard();
        }
    }
}