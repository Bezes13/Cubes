using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
        public TextMeshProUGUI result;
        [SerializeField]
        private PlayerMovement player;

        [SerializeField] private PathModel pathModel;
        [SerializeField] private PathGenerator generator;

        private void OnEnable()
        {
                int reachedPoints = (int) player.PlayerPos().z;
                result.text = $"You reached {reachedPoints} Points. Tschakalaka";
        }

        public void QuitGame()
        {
                Application.Quit();
        }

        public void Restart()
        { 
                pathModel.ResetModel();
                player.ResetPlayer();
                generator.CreatePath();
                gameObject.SetActive(false);
        }
}