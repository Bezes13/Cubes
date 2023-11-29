using Objects;
using Path;
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
        private static readonly Vector3 StartVec = new Vector3(-2, 0.5f, 5);

        [SerializeField] private PathObject pathObjectPrefab;
        [SerializeField] private PathObject BigCube;
        [SerializeField] private PathObject pyramidPrefab;
        [SerializeField] private PathObject logPrefab;
        [SerializeField] private PathObject starPrefab;
        [SerializeField] private PathObject coinPrefab;

        private Random _rnd;
        private PathGenerator _path;

        public enum PrefabType
        {
            Cube,
            BigCube,
            Log,
            Pyramid,
            Star,
            Coin
        }

        public void Init()
        {
            ResetModel();
            _rnd = new Random();
            _path = new GameObject("Path").AddComponent<PathGenerator>();
            _path.pathModel = this;
            _path.Init(StartVec);
        }

        public void CreatePathObject(PrefabType prefabType, Vector3 position)
        {
            var prefab = pathObjectPrefab;
            switch (prefabType)
            {
                case PrefabType.Pyramid:
                    prefab = pyramidPrefab;
                    break;
                case PrefabType.Log:
                    prefab = logPrefab;
                    break;
                case PrefabType.Cube:
                    prefab = pathObjectPrefab;
                    break;
                case PrefabType.BigCube:
                    prefab = BigCube;
                    var big = Instantiate(prefab, position + Vector3.up * 0.5f, Quaternion.identity, _path.transform);
                    big.name = position.x + "/" + position.z;
                    big.Init(_rnd.NextDouble());
                    return;
                case PrefabType.Coin:
                    prefab = coinPrefab;
                    break;
            }

            var obj = Instantiate(prefab, position, Quaternion.identity, _path.transform);
            obj.name = position.x + "/" + position.z;
            obj.Init(_rnd.NextDouble());
        }


        private void ResetModel()
        {
            if (_path == null)
            {
                return;
            }

            Destroy(_path.gameObject);
            _path = null;
        }
    }
}