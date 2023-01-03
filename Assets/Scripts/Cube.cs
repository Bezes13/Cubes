using UnityEngine;

public class Cube : MonoBehaviour
{
    private PlayerMovement _player;
    private PathGenerator pathGenerator;
    public PathModel pathModel;
        
    private bool _dieHard;
        
    private double _seed;
    private const float FallSpeed = 2;

    public void Init(double seed)
    {
        _seed = seed;
    }
    public void DieHard()
    {
        _dieHard = true;
        Destroy(gameObject, 1f);
        pathGenerator.ContinuePath();
    }

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
        pathGenerator = FindObjectOfType<PathGenerator>();
    }

    private void Update()
    {
        if (_dieHard)
        {
            var position = new Vector3(transform.position.x,
                transform.position.y - Time.deltaTime * FallSpeed, transform.position.z);
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