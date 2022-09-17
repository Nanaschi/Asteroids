using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private int _lives;
    [SerializeField] private int _respawnTime;
    [SerializeField] private int _timeOfInvincibility; //TODO: put it into the player

    public void PlayerDied()
    {
        _lives--;

        if (_lives <= 0) GameOver();
        else StartCoroutine(Respawn());
    }

    private void GameOver()
    {
        print("GameOver");
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_respawnTime);
        _player.transform.position = Vector3.zero;
        var boxCollider2D = _player.GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        _player.gameObject.SetActive(true);
        yield return new WaitForSeconds(_timeOfInvincibility);
        boxCollider2D.enabled = true;
    }
}
