using System;
using _Scripts.HighLevel;
using UnityEngine;

namespace _Scripts.UI
{
    public class GameUIController
    {
        private readonly GameUIView _gameUIView;

        public GameUIController(GameUIView gameUIView)
        {
            _gameUIView = gameUIView;
            OnEnable();
        }

        private void OnEnable()
        {
            GameManager.OnCurrentLivesChanged += UpdateLives;
            GameManager.OnScoreChanged += UpdateScore;
            Player.OnTransformChanged += UpdatePositionCoordinates;
            Player.OnTransformChanged += UpdateRotationCoordinates;
            Player.OnActiveVelocity += UpdateVelocity;
            Player.OnLaserFilled += UpdateLaserBar;
            Player.OnLaserChargeChanged += UpdateLaserCharges;
        }

        private void UpdateLaserCharges(int laserCharges)
        {
            _gameUIView.LaserCharges.text = laserCharges.ToString();
        }

        private float UpdateLaserBar(float fillPercent)
        {
            _gameUIView.LaserProgressBar.value = fillPercent;
            return _gameUIView.LaserProgressBar.value;
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
            _gameUIView.CurrentLives.text = updatedLives.ToString();
        }
    }
}