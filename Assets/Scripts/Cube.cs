using Signals;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private PlayerMovement _player;
    private PathGenerator pathGenerator;

    private bool _dieHard;
        
    private double _seed;
    private const float FallSpeed = 2;
    private Vector3 pos;
    private bool _split;

    public void Init(double seed, bool split = false)
    {
        _split = split;
        pos = transform.position;
        transform.position += Vector3.up;
        _seed = seed;
    }
    public void DieHard()
    {
        if (_split)
        {
            Supyrb.Signals.Get<DestroyPathSignal>().Dispatch();
        }
        _dieHard = true;
        Destroy(gameObject, 1f);
        pathGenerator.ContinuePath();
    }

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
        pathGenerator = FindObjectOfType<PathGenerator>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime*(Random.value + 1));
    }

    private void Update()
    {
        if (_dieHard)
        {
            var pos1 = transform.position;
            var position = new Vector3(pos1.x, pos1.y - Time.deltaTime * FallSpeed, pos1.z);
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