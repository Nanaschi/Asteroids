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

    [SerializeField] private Player _player;

    [SerializeField] private ParticleSystem _explosion;

    //TODO: Transfer lives into the player


    private void Awake()
    {
        _currentLives = _player.PlayerConfig.MaxLives;
    }

    public void PlayerDied() //TODO:Make it an event
    {
        _explosion.transform.position = _player.transform.position;
        _explosion.Play();

        _currentLives--;

        if (_currentLives <= 0) GameOver();
        else StartCoroutine(Respawn());
    }

    public void AsteroidDestroyed(Asteroid asteroid) //TODO:Make it an event
    {
        _explosion.transform.position = asteroid.transform.position;
        _explosion.Play();
        _score++;
    }

    private void GameOver()
    {
        print("GameOver");
        _currentLives = _player.PlayerConfig.MaxLives;
        _score = 0;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_player.PlayerConfig.RespawnTime);
        _player.transform.position = Vector3.zero;
        var boxCollider2D = _player.GetComponent<BoxCollider2D>();
        //TODO: Impplement invincibility here
        _player.gameObject.SetActive(true);
        yield return new WaitForSeconds(_player.PlayerConfig.TimeOfInvincibility);
    }
}