using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    #region DebugMode fields

    private int _currentLives;
    private int _score;

    #endregion

    [FormerlySerializedAs("_player")] [SerializeField] private Player _playerPrefab;

    [FormerlySerializedAs("_explosion")] [SerializeField] private ParticleSystem _explosionPrefab;

    //TODO: Transfer lives into the player

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
        _currentLives = _playerPrefab.PlayerConfig.MaxLives;
        _playerPrefab = Instantiate(_playerPrefab);
        _explosionPrefab = Instantiate(_explosionPrefab);
    }

    public void PlayerDied() //TODO:Make it an event
    {
        _explosionPrefab.transform.position = _playerPrefab.transform.position;
        _explosionPrefab.Play();

        _currentLives--;

        if (_currentLives <= 0) GameOver();
        else StartCoroutine(Respawn());
    }

    private void AsteroidDestroyed(Asteroid asteroid) //TODO:Make it an event
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