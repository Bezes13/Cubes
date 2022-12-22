using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
        public TextMeshProUGUI result;
        [SerializeField]
        private PlayerMovement player;

        public TextMeshProUGUI[] Names;
        public TextMeshProUGUI[] Scores;
        [SerializeField] private PathModel pathModel;
        [SerializeField] private PathGenerator generator;
        [SerializeField] private PointsObject PointsObject;
        private int newRank;
        public string PlayerName { get; set; }

        private void OnEnable()
        {
                PointsObject.AddScore();
                newRank = -1;
                int reachedPoints = PointsObject.GetPoints();
                result.text = $"You got {reachedPoints} Points. BoxBoxBox";
                var highScoreEntries = PointsObject.GetHighScores();
                for (int i = 0; highScoreEntries.Length > i ; i++)
                {
                        Names[i].text = $"{i+1}. {highScoreEntries[i].Name}";
                        Scores[i].text = highScoreEntries[i].Points.ToString();
                        if (highScoreEntries[i].New)
                        {
                                newRank = i;
                        }
                }
                UpdateNameOnHighScoreBoard();
        }

        public void UpdateNameOnHighScoreBoard()
        {
                if (newRank == -1)
                {
                        return;
                }
                Names[newRank].text = $"{newRank+1}. {PlayerName}";
        }

        public void QuitGame()
        {
                Application.Quit();
        }

        public void Restart()
        {
                PointsObject.SaveName(newRank, PlayerName);
                PointsObject.ResetPoints();
                pathModel.ResetModel();
                player.ResetPlayer();
                generator.CreatePath();
                gameObject.SetActive(false);
        }
}