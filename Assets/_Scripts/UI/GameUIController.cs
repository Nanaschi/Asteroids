using System;
using System.Text;
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
            Player.OnTransformChanged += UpdateTransform;
        }

        private void UpdateTransform(Vector2 vector2)
        {
            _gameUIView.XPlayerTransform.text = Math.Round(vector2.x, 2).ToString();
            _gameUIView.YPlayerTransform.text = Math.Round(vector2.y, 2).ToString();
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