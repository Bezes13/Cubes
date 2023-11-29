using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
        private const int Speed = 10;
        [SerializeField]
        private GameObject coin;
        private void Update()
        {
            transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
            coin.transform.Rotate(Vector3.right);
        }

        public void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}