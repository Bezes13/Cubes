using Objects;
using Signals;
using TMPro;
using UnityEngine;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        private const int StartAmount = 5;
        private int _inventar = 5;
        public GameObject shot;
        public Transform shotSpawn;
        [SerializeField] private TextMeshProUGUI ui;

        private AudioSource audioSource;

        // This is called when the bullet instance is created
        void Start()
        {
            //audioSource = GetComponent<AudioSource>();
            ui.text = _inventar.ToString();
            Supyrb.Signals.Get<StartGameSignal>().AddListener(StartGame);
        }

        private void StartGame()
        {
            _inventar = StartAmount;
            ui.text = _inventar.ToString();
        }

        public void Fire()
        {
            if (_inventar <= 0)
            {
                return;
            }
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            _inventar--;
            ui.text = _inventar.ToString();
            //audioSource.Play();
        }

        public void CoinCollected()
        {
            _inventar++;
            ui.text = _inventar.ToString();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var obj = other.GetComponent<CollectableCoin>();
            if (obj != null)
            {
                CoinCollected();
                Destroy(other.gameObject);
            }
        }
    }
}