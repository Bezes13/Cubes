using System.Collections.Generic;
using Model;
using Signals;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private const int MinPathElementCount = 100;
    public PathModel pathModel;
    private Vector3 _start;
    private Vector3 _nextPoint;
    public int pathNumber;
    public List<Cube> cubes;
    public int sidePath;

    private void Awake()
    {
        cubes = new List<Cube>();
        Supyrb.Signals.Get<RestartGameSignal>().AddListener(CreatePath);
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
        // float difficulty = pointsObject.GetPoints() / 1000f;
        // pathModel.RandomTripletAtLeastOne.Increase(difficulty);
    }

    public void ContinuePath()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(_nextPoint);
        bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 0.8;
        if (onScreen)
        {
            _nextPoint = pathModel.Chaos.Create(_nextPoint, pathNumber);
        }
    }

    private void CreatePath()
    {
        pathModel.TripleBlock.Create(_start, pathNumber);
        _nextPoint = pathModel.JustTriplets.Create(_start + new Vector3(0, 0, 1), pathNumber);
    }

    public void Init(int pathNumber, Vector3 start, int sidePath)
    {
        this.pathNumber = pathNumber;
        _start = start;
        this.sidePath = sidePath;
    }
}