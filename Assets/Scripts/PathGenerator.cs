using NonTerminals;
using Signals;
using Terminals;
using UI;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private const int MinPathElementCount = 100;
    public PathModel pathModel;
    public PointsObject PointsObject;
    private Vector3 _start;
    private Vector3 _nextPoint;
    private Vector3 _secondPathPoint;
    private bool _secondPath;
    private bool _IsSecondStartPoint;

    private void Awake()
    {
        pathModel.Init();
        _start = new Vector3(0, 0.5f, 5);
        CreatePath();
        
        Supyrb.Signals.Get<RestartGameSignal>().AddListener(CreatePath);
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
        float difficulty = PointsObject.GetPoints() / 1000f;
        pathModel.RandomTripletAtLeastOne.Increase(difficulty);
    }

    public void ContinuePath()
    {
        if (pathModel.path.transform.childCount < MinPathElementCount)
        {
            _nextPoint = pathModel.Chaos.Create(_nextPoint, 0);
        }
        if (_secondPath && pathModel.secondPath.transform.childCount < MinPathElementCount)
        {
            _secondPathPoint = pathModel.Chaos.Create(_secondPathPoint, 1);   
        }
    }

    private void CreatePath()
    {
        new TripleBlock(pathModel).Create(_start, 0);
        _nextPoint = new JustTriplets(pathModel).Create(_start + new Vector3(0, 0, 1), 0);
    }

    public void CreateSecondPath(Vector3 start)
    {
        _secondPath = true;
        _IsSecondStartPoint = true;
        pathModel.TripleBlock.Create(start, 1);
        _secondPathPoint = pathModel.Chaos.Create(start + new Vector3(0, 0, 1), 1);
    }
}