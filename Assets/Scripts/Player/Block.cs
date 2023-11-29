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
        [SerializeField]
        private Transform front;
        private AudioSource _audioSource;

        private PlayerMovement _player;
        
        private Vector3 _goTo;
        private bool _placed;
        private GameObject _lastHit;

        private void Start()
        {
            _player = FindObjectOfType<PlayerMovement>();
            _audioSource = GetComponent<AudioSource>();
            if (!Physics.Raycast(_player.transform.position, Vector3.down, 2))
            {
                _placed = true;
                transform.position = transform.position + Vector3.down*2 + Vector3.back;
                
            }
        }

        private void Update()
        {
            // After its placed, Destroy it, after the player passed it.
            if(_placed)
            {
                if (_player.PlayerPos().z > transform.position.z + 1.5)
                {
                    Destroy(gameObject, 1f);
                }
                return;
            }
            // When we are above an empty spot
            if (_goTo != default && _lastHit != default)
            {
                // Move over the spot
                if (transform.position.z < _goTo.z)
                {
                    transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
                    return;
                }
                // Get down in the spot
                if (transform.position.y > _goTo.y)
                {
                    transform.Translate(Vector3.down * (Speed * Time.deltaTime));
                    return;
                }
                transform.position = new Vector3(transform.position.x, _goTo.y, _goTo.z);
                _placed = true;
                _audioSource.Play();
                return;
            }
            
            transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
            if (Physics.Raycast(front.position, Vector3.forward,out var hit, 0.1f))
            {
                _placed = true;
                transform.position = new Vector3(transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z-1);
                return;
            }
            // Find the next Free Spot
            if (Physics.Raycast(bottom.position, Vector3.down, out hit))
            {
                _lastHit = hit.collider.gameObject;
                return;
            }

            if (_lastHit)
            {
                _goTo = _lastHit.transform.position + Vector3.forward;
            }
            else
            {
                _placed = true;
                transform.position = transform.position + Vector3.down*2 + Vector3.back;
            }
                
        }

        public void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}