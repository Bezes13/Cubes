using Player;
using UnityEngine;

namespace Objects
{
    public class Block : MonoBehaviour
    {
        private const int Speed = 10;
        [SerializeField]
        private GameObject block;
        [SerializeField]
        private Transform bottom;
        private AudioSource audioSource;

        private PlayerMovement _player;
        
        private Vector3 goTo;
        private bool placed;
        private GameObject lastHit;

        private void Start()
        {
            _player = FindObjectOfType<PlayerMovement>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            // After its placed, Destroy it, after the player passed it.
            if(placed)
            {
                if (_player.PlayerPos().z > transform.position.z + 1.5)
                {
                    Destroy(gameObject, 1f);
                }
                return;
            }
            // When we are above an empty spot
            if (goTo != default && lastHit != default)
            {
                // Move over the spot
                if (transform.position.z < goTo.z)
                {
                    transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
                    return;
                }
                // Get down in the spot
                if (transform.position.y > goTo.y)
                {
                    transform.Translate(Vector3.down * (Speed * Time.deltaTime));
                    return;
                }
                print(goTo);
                print("placed");
                transform.position = new Vector3(transform.position.x, goTo.y, goTo.z);
                placed = true;
                audioSource.Play();
                return;
            }
            
            transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
            // Find the next Free Spot
            if (Physics.Raycast(bottom.position, Vector3.down, out var hit))
            {
                lastHit = hit.collider.gameObject;
                return;
            }
            if(lastHit)
                goTo = lastHit.transform.position + Vector3.forward;
        }

        public void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}