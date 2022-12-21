using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Player.transform.position + new Vector3(1.18f, 5.33f, -6.31f);
        transform.Rotate(18,-9,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player = Player.PlayerPos();
        Vector3 newPos = new Vector3(player.x + 1.18f, player.y + 5.33f, player.z - 6.31f);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
    }
}
