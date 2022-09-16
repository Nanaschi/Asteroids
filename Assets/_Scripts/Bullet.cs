using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _bulletSpeed; //TODO: put into model

    public void Project(Vector2 objectDirection)
    {
        _rigidbody2D.AddForce(objectDirection * _bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Boundary>())
        {
            print("heh");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponentInParent<Boundary>())
        {
            Destroy(gameObject);
        }
    }
}
