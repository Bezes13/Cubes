using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "Model", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class PathModel : ScriptableObject
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Pyramid pyramidPrefab;
    private List<Cube> spawnedCubes;
    private Dictionary<int, List<Cube>> cubeMap;
    private Random _rnd;

    public enum Prefabtype
    {
        Cube,
        Pyramid
    }
        
        

    public void Init()
    {
        spawnedCubes = new List<Cube>();
        cubeMap = new Dictionary<int, List<Cube>>();
        _rnd = new Random();
    }

    public void CreateObject(Prefabtype prefabType, Vector3 position)
    {
        if (prefabType.Equals(Prefabtype.Pyramid))
        {
            var pyramid = Instantiate(pyramidPrefab, position, Quaternion.Euler(45,0,45));
            pyramid.Init(_rnd.NextDouble());
            return;
        }
        var obj = Instantiate(cubePrefab, position, Quaternion.identity);
            
        obj.Init(_rnd.NextDouble());
        spawnedCubes.Add(obj);
        var z = (int) position.z;
        if (cubeMap.ContainsKey(z))
        {
            cubeMap[z].Add(obj);
        }
        else
        {
            cubeMap.Add(z, new List<Cube>{obj});
        }
    }
}