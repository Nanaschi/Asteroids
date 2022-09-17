using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private int _lives;
    [SerializeField] private int _respawnTime;

    public void PlayerDied()
    {
        _lives--;

        if (_lives <= 0) GameOver();
        else Invoke(nameof(Respawn), _respawnTime);
    }

    private void GameOver()
    {
        print("GameOver");
    }

    public void Respawn()
    {
        _player.transform.position = Vector3.zero;
        
        _player.gameObject.SetActive(true);
    }
}
