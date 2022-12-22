using UnityEngine;

[CreateAssetMenu(fileName = "Points", menuName = "ScriptableObjects/PointsObject", order = 2)]
public class PointsObject : ScriptableObject
{
    private const string HighScore1 = "highScore1";
    private const string HighScore2 = "highScore2";
    private const string HighScore3 = "highScore3";
    private const string HighScore1Name = "highScore1Name";
    private const string HighScore2Name = "highScore2Name";
    private const string HighScore3Name = "highScore3Name";

    private int _points = 0;
    private int rank;
    public string PlayerName { get; set; }

    public int GetPoints()
    {
        return _points;
    }

    public void AddPoints(int points)
    {
        _points += points;
    }

    public void AddScore(int score = -1)
    {
        rank = -1;
        if (score == -1)
        {
            score = _points;
        }
        var highscore = PlayerPrefs.GetInt(HighScore1);
        if (score > highscore)
        {
            rank = 1;
            PlayerPrefs.SetInt(HighScore3, PlayerPrefs.GetInt(HighScore2));
            PlayerPrefs.SetString(HighScore3Name, PlayerPrefs.GetString(HighScore2Name));
            PlayerPrefs.SetInt(HighScore2, highscore);
            PlayerPrefs.SetString(HighScore2Name, PlayerPrefs.GetString(HighScore1Name));
            PlayerPrefs.SetInt(HighScore1, score);
            PlayerPrefs.SetString(HighScore1Name, "Player");
        }
        else
        {
            highscore = PlayerPrefs.GetInt(HighScore2);
            if (score > highscore)
            {
                rank = 2;
                PlayerPrefs.SetInt(HighScore3, highscore);
                PlayerPrefs.SetString(HighScore3Name, PlayerPrefs.GetString(HighScore2Name));
                PlayerPrefs.SetInt(HighScore2, score);
                PlayerPrefs.SetString(HighScore2Name, "Player");

            }
            else
            {
                highscore = PlayerPrefs.GetInt(HighScore3);
                if (score > highscore)
                {
                    rank = 3;
                    PlayerPrefs.SetInt(HighScore3, score);
                    PlayerPrefs.SetString(HighScore3Name, "Player");
                }
            }
        }
    }

    public struct HighScoreEntry
    {
        public string Name;
        public int Points;
        public bool New;
    }
    public HighScoreEntry[] GetHighScores()
    {
        return new HighScoreEntry[]
            {new HighScoreEntry()
            {
                Name   = PlayerPrefs.GetString(HighScore1Name),
                Points = PlayerPrefs.GetInt(HighScore1),
                New = 1 == rank
                
            },new HighScoreEntry()
            {
                Name   = PlayerPrefs.GetString(HighScore2Name),
                Points = PlayerPrefs.GetInt(HighScore2),
                New = 2 == rank
            },new HighScoreEntry()
            {
                Name   = PlayerPrefs.GetString(HighScore3Name),
                Points = PlayerPrefs.GetInt(HighScore3),
                New = 3 == rank
            }};
    }

    public void ResetPoints()
    {
        _points = 0;
    }

    public void SaveName(int newRank, string inputFieldText)
    {
        if (newRank == 0)
        {
            PlayerPrefs.SetString(HighScore1Name, inputFieldText);
        }
        if (newRank == 1)
        {
            PlayerPrefs.SetString(HighScore2Name, inputFieldText);
        }
        if (newRank == 2)
        {
            PlayerPrefs.SetString(HighScore3Name, inputFieldText);
        }
    }
}