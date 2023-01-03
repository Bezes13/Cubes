using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;
    // Start is called before the first frame update
    private bool _central;
    void Start()
    {
        transform.position = player.transform.position + new Vector3(1.18f, 5.33f, -6.31f);
        transform.Rotate(18,-9,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _central = !_central;
            transform.rotation = _central ? Quaternion.Euler(18, 0, 0) :Quaternion.Euler(18, -9, 0);
        }
        Vector3 playerPos = player.PlayerPos();
        Vector3 newPos = new Vector3(playerPos.x + (_central ? 0 : 1.18f), playerPos.y + 5.33f, playerPos.z - 6.31f);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
    }
}
