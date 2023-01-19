using Model;
using Player;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// this class handles the start and result screen, with the start button and the highScore board
    /// </summary>
    public class ResultScreen : MonoBehaviour
    {
        public string PlayerName { get; set; }
        [SerializeField] private PathModel pathModel;
        [SerializeField] private PointsObject pointsObject;
        [SerializeField] private TextMeshProUGUI result;
        [SerializeField] private PlayerMovement player;
        [SerializeField] private TextMeshProUGUI[] names;
        [SerializeField] private TextMeshProUGUI[] scores;
        [SerializeField] private Text startButton;
        [SerializeField] private GameObject image;
        [SerializeField] private TMP_InputField inputField;

        private int _newRank;

        private void Awake()
        {
            Supyrb.Signals.Get<PlayerDeadSignal>().AddListener(ShowScreen);
            startButton.text = "Start";
            result.gameObject.SetActive(false);
            image.SetActive(false);
            inputField.gameObject.SetActive(false);
        }

        private void ShowScreen()
        {
            pointsObject.AddScore();
            gameObject.SetActive(true);
            startButton.text = "Restart";
            result.gameObject.SetActive(true);
            image.SetActive(true);
            inputField.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Restart();
            }
        }

        private void OnEnable()
        {
            _newRank = -1;
            var reachedPoints = pointsObject.GetPoints();
            var highScoreEntries = pointsObject.GetHighScores();
            for (var i = 0; highScoreEntries.Count > i; i++)
            {
                names[i].text = $"{(highScoreEntries[i].Name == "" ? "Player" : highScoreEntries[i].Name)}";
                scores[i].text = highScoreEntries[i].Points.ToString();
                if (highScoreEntries[i].New)
                {
                    _newRank = i;
                }
            }

            result.text = $"You got {reachedPoints} Points.";
            UpdateNameOnHighScoreBoard();
        }

        public void UpdateNameOnHighScoreBoard()
        {
            if (_newRank == -1)
            {
                return;
            }

            names[_newRank].text = PlayerName;
            pointsObject.SaveName(_newRank, PlayerName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void Restart()
        {
            pointsObject.ResetPoints();
            pathModel.Init();
            player.ResetPlayer();
            gameObject.SetActive(false);
            Supyrb.Signals.Get<StartGameSignal>().Dispatch();
        }
    }
}