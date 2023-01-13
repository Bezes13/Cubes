using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    private PlayerMovement _player;
    private PathGenerator pathGenerator;

    private bool _dieHard;

    private double _seed;
    private const float FallSpeed = 2;
    public Vector3 pos;
    private bool _split;
    private int _pathNumber;

    public void Init(double seed, int pathNumber, PathGenerator pathGenerator1, bool split = false)
    {
        _pathNumber = pathNumber;
        _split = split;
        pos = transform.position;
        transform.position += Vector3.up;
        _seed = seed;
        pathGenerator = pathGenerator1;
    }

    private void DieHard()
    {
        if (_split)
        {
            Supyrb.Signals.Get<DestroyPathWarningSignal>().Dispatch();
        }

        _dieHard = true;
        Destroy(gameObject, 1f);
        if (pathGenerator)
        {
            pathGenerator.cubes.Remove(this);
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

    public int GetPathNumber()
    {
        return _pathNumber;
    }

    public void Kill()
    {
        _dieHard = true;
        Destroy(gameObject, 1f + (float) _seed);
    }
}