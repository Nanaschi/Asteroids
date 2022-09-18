using UnityEngine;
using Zenject;

namespace _Scripts
{
    public class GameUIController
    {
        private readonly GameUIView _gameUIView;
        
        public GameUIController(GameUIView gameUIView)
        {
            _gameUIView = gameUIView;
            GameManager.OnCurrentLivesChanged += UpdateLives;
            GameManager.OnScoreChanged += UpdateScore;
        }

        private void UpdateScore(int updatedScore)
        {
            _gameUIView.CurrentScore.text = updatedScore.ToString();
        }

        private void UpdateLives(int updatedLives)
        {
            Debug.Log("Update Lives");
            _gameUIView.CurrentLives.text = updatedLives.ToString();
        }
    }
}