using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;
    
    public Vector3 offset = new Vector3(0f, 3f, -3.5f);

    void Start()
    {
        transform.position = player.transform.position + offset;
        transform.Rotate(30,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.PlayerPos();
        Vector3 newPos = new Vector3(playerPos.x , playerPos.y , playerPos.z) + offset;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
    }
}
