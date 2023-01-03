using Signals;
using TMPro;
using UnityEngine;

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
                
                private int _newRank;

                private void Awake()
                {
                        Supyrb.Signals.Get<PlayerDeadSignal>().AddListener(ShowScreen);
                        gameObject.SetActive(false);
                }

                private void ShowScreen()
                {
                        gameObject.SetActive(true);
                }

                private void OnEnable()
                {
                        pointsObject.AddScore();
                        _newRank = -1;
                        int reachedPoints = pointsObject.GetPoints();
                        var highScoreEntries = pointsObject.GetHighScores();
                        for (int i = 0; highScoreEntries.Length > i ; i++)
                        {
                                names[i].text = $"{i+1}. {highScoreEntries[i].Name}";
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
                        names[_newRank].text = $"{_newRank+1}. {PlayerName}";
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
                        pathModel.ResetModel();
                        player.ResetPlayer();
                        //generator.CreatePath();
                        Supyrb.Signals.Get<RestartGameSignal>().Dispatch();
                        gameObject.SetActive(false);
                }
        }
}