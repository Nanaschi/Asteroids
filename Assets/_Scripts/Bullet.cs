using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _bulletSpeed; //TODO: put into model

    public Action<Bullet> OnBulletCollided;
    public void Project(Vector2 objectDirection)
    {
        _rigidbody2D.AddForce(objectDirection * _bulletSpeed);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponentInParent<Boundary>())
        {
            OnBulletCollided?.Invoke(this);
        }
    }
}
