using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLives;
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _xPlayerTransform;
    [SerializeField] private TextMeshProUGUI _yPlayerTransform;
    [SerializeField] private TextMeshProUGUI _playerRotationAngle;
    [SerializeField] private TextMeshProUGUI _playerVelocity;
    [SerializeField] private Slider _laserProgressBar;
    [SerializeField] private TextMeshProUGUI _laserCharges;

    public TextMeshProUGUI PlayerVelocity
    {
        get => _playerVelocity;
        set => _playerVelocity = value;
    }

    public Slider LaserProgressBar
    {
        get => _laserProgressBar;
        set => _laserProgressBar = value;
    }

    public TextMeshProUGUI LaserCharges
    {
        get => _laserCharges;
        set => _laserCharges = value;
    }

    public TextMeshProUGUI PlayerRotationAngle
    {
        get => _playerRotationAngle;
        set => _playerRotationAngle = value;
    }

    public TextMeshProUGUI XPlayerTransform
    {
        get => _xPlayerTransform;
        set => _xPlayerTransform = value;
    }

    public TextMeshProUGUI YPlayerTransform
    {
        get => _yPlayerTransform;
        set => _yPlayerTransform = value;
    }

    public TextMeshProUGUI CurrentLives
    {
        get => _currentLives;
        set => _currentLives = value;
    }

    public TextMeshProUGUI CurrentScore
    {
        get => _currentScore;
        set => _currentScore = value;
    }
}
