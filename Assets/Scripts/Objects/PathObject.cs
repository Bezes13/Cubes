using Path;
using Player;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
    /// <summary>
    /// PathObject which is Part of the Path which gets created by the grammar, if the player passes the object,
    /// it will get destroyed after a short delay
    /// </summary>
    public class PathObject : MonoBehaviour
    {
        private const float FallSpeed = 2;

        public Vector3 pos;

        private PlayerMovement _player;
        private PathGenerator _pathGenerator;
        private bool _dieHard;
        private double _seed;
        private bool _split;

        private int _pathNumber;

        public void Init(double seed, int pathNumber, PathGenerator pathGenerator1, bool split = false)
        {
            _pathNumber = pathNumber;
            _split = split;
            pos = transform.position;
            transform.position += Vector3.up;
            _seed = seed;
            _pathGenerator = pathGenerator1;
        }

        public int GetPathNumber()
        {
            return _pathNumber;
        }

        public void Kill()
        {
            _dieHard = true;
            Destroy(gameObject, 1f + (float) _seed);
        }

        private void DieHard()
        {
            if (_split)
            {
                Supyrb.Signals.Get<DestroyPathWarningSignal>().Dispatch();
            }

            _dieHard = true;
            Destroy(gameObject, 1f);
            if (_pathGenerator)
            {
                _pathGenerator.cubes.Remove(this);
            }
        }

        private void OnDestroy()
        {
            if (_pathGenerator)
            {
                _pathGenerator.cubes.Remove(this);
            }
        }

        private void Awake()
        {
            _player = FindObjectOfType<PlayerMovement>();
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * (Random.value + 1));
        }

        private void Update()
        {
            if (_dieHard)
            {
                var pos1 = transform.position;
                var position = new Vector3(pos1.x, pos1.y - Time.deltaTime * FallSpeed * (float) _seed, pos1.z);
                transform.position = position;
            }
            else
            {
                if (_player.PlayerPos().z > transform.position.z + 1 + _seed)
                {
                    DieHard();
                }
            }
        }
    }
}