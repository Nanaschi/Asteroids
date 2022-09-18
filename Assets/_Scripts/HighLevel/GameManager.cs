using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using _Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

public class GameManager : MonoBehaviour
{
    #region DebugMode fields

    

    public static event Action<int> OnCurrentLivesChanged;
    public static event Action<int> OnScoreChanged;

    private int _currentLives;
    private int CurrentLives
    {
        get => _currentLives;
        set
        {
            _currentLives = value;
            OnCurrentLivesChanged?.Invoke(_currentLives);
        }
    }


    private int _score;

    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        } 
    }

    #endregion

    private Player _playerPrefab;

    [SerializeField] private ParticleSystem _explosionPrefab;

    private Player.Factory _playerFactory;
    private GameUIView _gameUIView;

    [Inject]
    public void InitInject(Player.Factory playerFactory, GameUIView gameUIView)
    {
        _gameUIView = gameUIView;
        _playerFactory = playerFactory;
    }
    
    private void OnEnable()
    {
        Asteroid.OnAsteroidDestroyed += AsteroidDestroyed;
        Player.OnAsteroidCollided += PlayerDied;
    }

    private void OnDisable()
    {
        Asteroid.OnAsteroidDestroyed -= AsteroidDestroyed;
        Player.OnAsteroidCollided -= PlayerDied;
    }

    private void Awake()
    {
        Score = 0;
        _playerPrefab = _playerFactory.Create();
        CurrentLives = _playerPrefab.PlayerConfig.MaxLives;


        _explosionPrefab = Instantiate(_explosionPrefab);
    }

    private void PlayerDied()
    {
        _explosionPrefab.transform.position = _playerPrefab.transform.position;
        _explosionPrefab.Play();

        CurrentLives--;

        if (CurrentLives <= 0) GameOver();
        else StartCoroutine(Respawn());
    }

    private void AsteroidDestroyed(Asteroid asteroid)
    {
        print("AsteroidDestroyed");
        _explosionPrefab.transform.position = asteroid.transform.position;
        _explosionPrefab.Play();
        Score++;
    }

    private void GameOver()
    {
        print("GameOver");
        Invoke(nameof(ReloadCurrentScene),_playerPrefab.PlayerConfig.RespawnTime);
        
        
    }

    private void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_playerPrefab.PlayerConfig.RespawnTime);
        _playerPrefab.transform.position = Vector3.zero;
        var boxCollider2D = _playerPrefab.GetComponent<BoxCollider2D>();
        //TODO: Impplement invincibility here
        _playerPrefab.gameObject.SetActive(true);
        yield return new WaitForSeconds(_playerPrefab.PlayerConfig.TimeOfInvincibility);
    }
}