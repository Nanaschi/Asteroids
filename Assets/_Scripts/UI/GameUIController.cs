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
            Player.OnTransformChanged += UpdatePositionCoordinates;
            Player.OnTransformChanged += UpdateRotationCoordinates;
            Player.OnActiveVelocity += UpdateVelocity;
        }

        private void UpdateVelocity(Rigidbody2D rigidbody2D)
        {
            _gameUIView.PlayerVelocity.text = 
                Math.Round(rigidbody2D.velocity.magnitude, 2).ToString();
        }

        private void UpdateRotationCoordinates(Transform transform)
        {
            _gameUIView.PlayerRotationAngle.text =
                Math.Round(transform.rotation.eulerAngles.z, 1).ToString();
        }

        private void UpdatePositionCoordinates(Transform transform)
        {
            _gameUIView.XPlayerTransform.text = Math.Round(transform.position.x, 2).ToString();
            _gameUIView.YPlayerTransform.text = Math.Round(transform.position.y, 2).ToString();
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