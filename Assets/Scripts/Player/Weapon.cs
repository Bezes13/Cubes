using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        private const int StartAmount = 5;
        private int _coinInventory = 5;
        private int _blockInventory = 5;
        public GameObject coin;
        public GameObject block;
        public Transform shotSpawn;
        public Transform blockSpawn;
        [SerializeField] private TextMeshProUGUI coinAmount;
        [SerializeField] private TextMeshProUGUI blockAmount;
        [SerializeField] private Image coinButton;
        [SerializeField] private Image blockButton;
        private List<GameObject> spawned;
        private const float Cooldown = 1f;

        private AudioSource audioSource;
        [SerializeField] private AudioClip blockSound;
        [SerializeField] private AudioClip coinSound;
        private bool _coinReloading;
        private bool _blockReloading;
        private double _blockTimeOut;
        private double _coinTimeOut;

        // This is called when the bullet instance is created
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            spawned = new List<GameObject>();
            coinAmount.text = _coinInventory.ToString();
            blockAmount.text = _blockInventory.ToString();
            Supyrb.Signals.Get<StartGameSignal>().AddListener(StartGame);
        }

        private void Update()
        {
            blockButton.fillAmount = 1 - (float)_blockTimeOut;
            coinButton.fillAmount = 1 - (float)_coinTimeOut;
        }

        private void StartGame()
        {
            _coinInventory = StartAmount;
            _blockInventory = StartAmount;
            coinAmount.text = _coinInventory.ToString();
            blockAmount.text = _blockInventory.ToString();
            foreach (var spawnedBlock in spawned)
            {
                Destroy(spawnedBlock);
            }

            spawned.Clear();
        }

        public void CollectBlock()
        {
            _blockInventory++;
            blockAmount.text = _blockInventory.ToString();
        }

        public void Fire()
        {
            if (_coinInventory <= 0 || _coinReloading)
            {
                return;
            }

            Instantiate(coin, shotSpawn.position, shotSpawn.rotation);
            _coinInventory--;
            coinAmount.text = _coinInventory.ToString();
            audioSource.clip = coinSound;
            audioSource.Play();
            StartCoroutine(Countdown(true));
        }

        public void PlaceBlock()
        {
            if (_blockInventory <= 0 || _blockReloading)
            {
                return;
            }

            var obj = Instantiate(block, blockSpawn.position, blockSpawn.rotation);
            spawned.Add(obj);
            _blockInventory--;
            blockAmount.text = _blockInventory.ToString();
            audioSource.clip = blockSound;
            audioSource.Play();
            StartCoroutine(Countdown(false));
        }

        private void CoinCollected()
        {
            _coinInventory++;
            coinAmount.text = _coinInventory.ToString();
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

        private IEnumerator Countdown(bool isCoin)
        {
            float normalizedTime = 0;
            if (isCoin)
            {
                _coinReloading = true;
                while (normalizedTime <= 1f)
                {
                    _coinTimeOut = Math.Round(Cooldown - Math.Round(Cooldown * normalizedTime, 1), 1);
                    normalizedTime += Time.deltaTime / Cooldown;
                    yield return null;
                }

                _coinReloading = false;
            }
            else
            {
                _blockReloading = true;
                while (normalizedTime <= 1f)
                {
                    _blockTimeOut = Math.Round(Cooldown - Math.Round(Cooldown * normalizedTime, 1), 1);

                    normalizedTime += Time.deltaTime / Cooldown;
                    yield return null;
                }

                _blockReloading = false;
            }
        }
    }
}