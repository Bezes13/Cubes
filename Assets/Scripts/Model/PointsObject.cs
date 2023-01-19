using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// Scriptable Object which stores the Information about the current Points and the 3 best scores.
    /// And the functionality to add more Points and save the scores in the PlayerPrefs.
    /// </summary>
    [CreateAssetMenu(fileName = "Points", menuName = "ScriptableObjects/PointsObject", order = 2)]
    public class PointsObject : ScriptableObject
    {
        private static readonly List<string> HighScoreStrings = new List<string>() {"highScore1", "highScore2", "highScore3"};
        private static readonly List<string> HighScoreNameStrings = new List<string>() {"highScore1Name", "highScore2Name", "highScore3Name"};

        private int _points;
        private int _rank;
        
        public struct HighScoreEntry
        {
            public string Name;
            public int Points;
            public bool New;
        }

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
            _rank = -1;
            if (score == -1)
            {
                score = _points;
            }

            var highScore = PlayerPrefs.GetInt(HighScoreStrings[0]);
            if (score > highScore)
            {
                PlayerPrefs.SetInt(HighScoreStrings[2], PlayerPrefs.GetInt(HighScoreStrings[1]));
                PlayerPrefs.SetString(HighScoreNameStrings[2], PlayerPrefs.GetString(HighScoreNameStrings[1]));
                PlayerPrefs.SetInt(HighScoreStrings[1], highScore);
                PlayerPrefs.SetString(HighScoreNameStrings[1], PlayerPrefs.GetString(HighScoreNameStrings[0]));
                SetScore(0);
            }
            else
            {
                highScore = PlayerPrefs.GetInt(HighScoreStrings[1]);
                if (score > highScore)
                {
                    PlayerPrefs.SetInt(HighScoreStrings[2], highScore);
                    PlayerPrefs.SetString(HighScoreNameStrings[2], PlayerPrefs.GetString(HighScoreNameStrings[1]));
                    SetScore(1);
                }
                else
                {
                    highScore = PlayerPrefs.GetInt(HighScoreStrings[2]);
                    if (score <= highScore) return;
                    SetScore(2);
                }
            }

            void SetScore(int i)
            {
                PlayerPrefs.SetInt(HighScoreStrings[i], score);
                PlayerPrefs.SetString(HighScoreNameStrings[i], "Player");
                _rank = i+1;
            }
        }

        public List<HighScoreEntry> GetHighScores()
        {
            var result = new List<HighScoreEntry>();
            for (var i = 0; i < 3; i++)
            {
                result.Add(new HighScoreEntry()
                {
                    Name = PlayerPrefs.GetString(HighScoreNameStrings[i]),
                    Points = PlayerPrefs.GetInt(HighScoreStrings[i]),
                    New = i + 1 == _rank
                });
            }

            return result;
        }

        public void ResetPoints()
        {
            _points = 0;
        }

        public void SaveName(int newRank, string inputFieldText)
        {
            PlayerPrefs.SetString(HighScoreNameStrings[newRank], inputFieldText);
        }
    }
}