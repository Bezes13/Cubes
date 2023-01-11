using Model;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
        public class ResultScreen : MonoBehaviour
        {
                private const string NewHighscore = "New Highscore!";
                public string PlayerName { get; set; }
                [SerializeField] 
                private PathModel pathModel;
                [SerializeField] 
                private PointsObject pointsObject;
                [SerializeField] 
                private TextMeshProUGUI result;
                [SerializeField]
                private PlayerMovement player;
                [SerializeField]
                private TextMeshProUGUI[] names;
                [SerializeField]
                private TextMeshProUGUI[] scores;
                [SerializeField]
                private Text startButton;
                [SerializeField]
                private GameObject Image;
                [SerializeField]
                private TMP_InputField inputField;

                private int _newRank;

                private void Awake()
                {
                        Supyrb.Signals.Get<PlayerDeadSignal>().AddListener(ShowScreen);
                        startButton.text = "Start";
                        result.gameObject.SetActive(false);
                        Image.SetActive(false);
                        inputField.gameObject.SetActive(false);
                }

                private void ShowScreen()
                {
                        pointsObject.AddScore();
                        gameObject.SetActive(true);
                        startButton.text = "Restart";
                        result.gameObject.SetActive(true);
                        Image.SetActive(true);
                        inputField.gameObject.SetActive(true);
                }

                private void Update()
                {
                        if (Input.GetKeyDown(KeyCode.P))
                        {
                                Restart();
                        }
                }

                private void OnEnable()
                {
                        _newRank = -1;
                        int reachedPoints = pointsObject.GetPoints();
                        var highScoreEntries = pointsObject.GetHighScores();
                        for (int i = 0; highScoreEntries.Length > i ; i++)
                        {
                                names[i].text = $"{i+1}. {highScoreEntries[i].Name ?? "Player1"}";
                                scores[i].text = highScoreEntries[i].Points.ToString();
                                if (highScoreEntries[i].New)
                                {
                                        _newRank = i;
                                }
                        }

                        var text = _newRank == 0 ? NewHighscore : PlayerPrefs.GetString(PointsObject.HighScore1) + " is better.";
                        result.text = $"You got {reachedPoints} Points. {text}";
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
                        pointsObject.SaveName(_newRank, PlayerName);
                        pointsObject.ResetPoints();
                        pathModel.Init();
                        player.ResetPlayer();
                        Supyrb.Signals.Get<RestartGameSignal>().Dispatch();
                        gameObject.SetActive(false);
                        Supyrb.Signals.Get<StartGameSignal>().Dispatch();
                }
        }
}