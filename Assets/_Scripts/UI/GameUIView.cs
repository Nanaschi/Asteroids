using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLives;
    [SerializeField] private TextMeshProUGUI _currentScore;

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
