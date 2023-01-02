using NonTerminals;
using Terminals;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathGenerator : MonoBehaviour
{
    private const int MinPathElementCount = 100;
    public PathModel pathModel;
    public PointsObject PointsObject;
    private Vector3 _start;
    private Vector3 _nextPoint;
    
    private void Awake()
    {
        pathModel.Init();
        _start = new Vector3(0, 0.5f, 5);
        CreatePath();
    }

    private void Update()
    {
        float difficulty = PointsObject.GetPoints() / 1000f;
        pathModel.RandomTripletAtLeastOne.Increase(difficulty);
    }

    public void ContinuePath()
    {
        if (pathModel.path.transform.childCount < MinPathElementCount)
        {
            _nextPoint = pathModel.Chaos.Create(_nextPoint);   
        }
    }

    public void CreatePath()
    {
        new TripleBlock(pathModel).Create(_start + new Vector3(0, 0, 0));
        _nextPoint = new JustTriplets(pathModel).Create(_start + new Vector3(0, 0, 1));
    }
        

    private Vector3 ChooseYourWeapon(Vector3 vec)
    {
        if (Random.value <= 0.3)
        {
            return new JustTriplets(pathModel).Create(vec);
        }
        
        return new Chaos(pathModel).Create(vec);
    }
}