using System;
using System.Collections;
using Model;
using Signals;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// this class handles the player ui, which shows the actual points and a warning if one side path will be destroyed
    /// </summary>
    public class PlayersUi : MonoBehaviour
    {
        private const float Duration = 3f;
        
        [SerializeField] private TextMeshProUGUI currentPoints;
        [SerializeField] private TextMeshProUGUI pathWarning;
        [SerializeField] private PointsObject pointsObject;
        [SerializeField] private GameObject warningImage;
        [SerializeField] private GameObject PauseText;
        [SerializeField] private GameObject ContinueButton;

        private bool _isActive;

        private void Awake()
        {
            currentPoints.text = "0";
            Supyrb.Signals.Get<DestroyPathWarningSignal>().AddListener(Warning);
        }

        public void Update()
        {
            currentPoints.text = pointsObject.GetPoints().ToString();
        }

        public void PauseGame()
        {
            Supyrb.Signals.Get<PauseSignal>().Dispatch();
            PauseText.SetActive(!PauseText.activeSelf);
            ContinueButton.SetActive(!ContinueButton.activeSelf);
        }

        public void UnPauseGame()
        {
            Supyrb.Signals.Get<UnPauseSignal>().Dispatch();
            PauseText.SetActive(false);
            ContinueButton.SetActive(false);
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
            warningImage.gameObject.SetActive(true);
        }

        private IEnumerator Countdown()
        {
            float normalizedTime = 0;
            while (normalizedTime <= 1f)
            {
                pathWarning.text = $"Second Path disappears in {3f - Math.Round(3f * normalizedTime)}s";
                normalizedTime += Time.deltaTime / Duration;
                yield return null;
            }

            pathWarning.gameObject.SetActive(false);
            warningImage.gameObject.SetActive(false);
            _isActive = false;
            Supyrb.Signals.Get<DestroyPathSignal>().Dispatch();
        }
    }
}