using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
        public TextMeshProUGUI result;
        [SerializeField]
        private PlayerMovement player;
        private const string NewHighscore = "New Highscore!";
        public TextMeshProUGUI[] names;
        public TextMeshProUGUI[] scores;
        [SerializeField] private PathModel pathModel;
        [SerializeField] private PathGenerator generator;
        [SerializeField] private PointsObject pointsObject;
        private int _newRank;
        public string PlayerName { get; set; }

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
                generator.CreatePath();
                gameObject.SetActive(false);
        }
}