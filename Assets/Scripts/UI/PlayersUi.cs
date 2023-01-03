using System;
using System.Collections;
using Signals;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayersUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentPoints;
        [SerializeField] private TextMeshProUGUI pathWarning;
        [SerializeField] private PointsObject pointsObject;
        [SerializeField] private PathModel pathModel;

        private bool _isActive;

        private void Awake()
        {
            currentPoints.text = "0";
            Supyrb.Signals.Get<DestroyPathSignal>().AddListener(Warning);
        }
        
        void Update()
        {
            currentPoints.text = "Points: " + pointsObject.GetPoints();
        }

        private void Warning()
        {
            if (_isActive)
            {
                return;
            }

            _isActive = true;
            StartCoroutine(Countdown());
            pathWarning.gameObject.SetActive(true);
        }
    
        private IEnumerator Countdown()
        {
            float duration = 3f; // 3 seconds you can change this 
            float normalizedTime = 0;
            while(normalizedTime <= 1f)
            {
                pathWarning.text = $"The second Path disappears in { 3f - Math.Round(3f * normalizedTime)} seconds"  ;
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }
            pathWarning.gameObject.SetActive(false);
            _isActive = false;
            pathModel.DestroyPath();
        }
    }
}
