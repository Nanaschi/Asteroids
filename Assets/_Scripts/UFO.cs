using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour, ITrajectable
{
    [SerializeField] private Player _player;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }


    public void SetTrajectory(Vector2 destination)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination,
            moveSpeed * Time.deltaTime);
    }
}