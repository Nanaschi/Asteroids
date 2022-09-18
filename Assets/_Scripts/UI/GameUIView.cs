using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLives;
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _xPlayerTransform;
    [SerializeField] private TextMeshProUGUI _yPlayerTransform;
    [SerializeField] private TextMeshProUGUI _playerRotationAngle;

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
