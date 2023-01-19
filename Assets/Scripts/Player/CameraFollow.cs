using UnityEngine;

namespace Player
{
    /// <summary>
    /// This class handles the camera movement
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private PlayerMovement player;

        public Vector3 offset = new Vector3(0f, 3f, -3.5f);

        private void Start()
        {
            transform.position = player.transform.position + offset;
            transform.Rotate(30, 0, 0);
        }

        private void Update()
        {
            var playerPos = player.PlayerPos();
            var newPos = new Vector3(playerPos.x, playerPos.y, playerPos.z) + offset;
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
        }
    }
}