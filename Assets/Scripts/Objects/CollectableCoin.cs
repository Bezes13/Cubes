using UnityEngine;

namespace Objects
{
    public class CollectableCoin : MonoBehaviour
    {
        [SerializeField] private GameObject coin;

        private void Update()
        {
            transform.Rotate(Vector3.up * 2);
        }
    }
}