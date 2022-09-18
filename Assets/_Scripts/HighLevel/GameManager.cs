using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameManager : MonoBehaviour
{
    #region DebugMode fields

    private int _currentLives;
    private int _score;

    #endregion

    private Player _playerPrefab;

    [SerializeField] private ParticleSystem _explosionPrefab;

    private Player.Factory _playerFactory;

    [Inject]
    public void InitInject(Player.Factory playerFactory)
    {
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
        _playerPrefab = _playerFactory.Create();
        _currentLives = _playerPrefab.PlayerConfig.MaxLives;


        _explosionPrefab = Instantiate(_explosionPrefab);
    }

    private void PlayerDied()
    {
        _explosionPrefab.transform.position = _playerPrefab.transform.position;
        _explosionPrefab.Play();

        _currentLives--;

        if (_currentLives <= 0) GameOver();
        else StartCoroutine(Respawn());
    }

    private void AsteroidDestroyed(Asteroid asteroid)
    {
        print("AsteroidDestroyed");
        _explosionPrefab.transform.position = asteroid.transform.position;
        _explosionPrefab.Play();
        _score++;
    }

    private void GameOver()
    {
        print("GameOver");
        _currentLives = _playerPrefab.PlayerConfig.MaxLives;
        _score = 0;
        StartCoroutine(Respawn());
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