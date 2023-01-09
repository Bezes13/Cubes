using System.Collections.Generic;
using Model;
using NonTerminals;
using Signals;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private const int MinPathElementCount = 100;
    public PathModel pathModel;
    public Vector3 start;
    private Grammar _nextPoint;
    public int pathNumber;
    public List<Cube> cubes;
    public int sidePath;
    public PathModel.Prefabtype type;

    private void Awake()
    {
        cubes = new List<Cube>();
        Supyrb.Signals.Get<RestartGameSignal>().AddListener(CreatePath);
    }

    public void SetType(PathModel.Prefabtype newType)
    {
        type = newType;
    }

    private void Start()
    {
        CreatePath();
    }

    private void OnDestroy()
    {
        Supyrb.Signals.Get<RestartGameSignal>().RemoveListener(CreatePath);
    }
    // Build Path when in sight
    // Destroy second Path
    // introduce messageBus
    // Handle secondStartPoint in cubes

    private void Update()
    {
        ContinuePath();
        // float difficulty = pointsObject.GetPoints() / 1000f;
        // pathModel.RandomTripletAtLeastOne.Increase(difficulty);
    }

    public void ContinuePath()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(_nextPoint.NextPoint);
        bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 0.8;
        if (onScreen)
        {
            _nextPoint = pathModel.Chaos.Create(_nextPoint.NextPoint, pathNumber);
            switch (_nextPoint.Part)
            {
                case PathPart.LeftSweep: 
                    _nextPoint =  pathModel.LeftSweep.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.RightSweep: 
                    _nextPoint = pathModel.RightSweep.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.Hole: 
                    _nextPoint = pathModel.Hole.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.SingleSpike: 
                    _nextPoint = pathModel.SingleSpike.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.TripleBlock: 
                    _nextPoint = pathModel.RandomTripletAtLeastOne.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.UpStairs: 
                    _nextPoint = pathModel.UpStairs.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.NoHoleNoSpike:
                    _nextPoint = pathModel.NoHoleOrSpike.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.AfterSweep:
                    _nextPoint = pathModel.AfterSweep.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.AfterSpikeOrHole:
                    _nextPoint = pathModel.AfterSpikeOrHole.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.PathSplitter:
                    _nextPoint = pathModel.PathSplitter.Create(_nextPoint.NextPoint, pathNumber);
                    return;
                case PathPart.RandomTripleAtLeastOne:
                    _nextPoint = pathModel.RandomTripletAtLeastOne.Create(_nextPoint.NextPoint, pathNumber);
                    return;
            }
            _nextPoint = pathModel.Chaos.Create(_nextPoint.NextPoint, pathNumber);
        }
    }

    private void CreatePath()
    {
        pathModel.TripleBlock.Create(start, pathNumber);
        _nextPoint = pathModel.BlockPart.Create(start + new Vector3(0, 0, 1), pathNumber);
    }

    public void Init(int pathNumber, Vector3 start, int sidePath)
    {
        this.pathNumber = pathNumber;
        this.start = start;
        this.sidePath = sidePath;
    }
}