using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    //too small for a separate Scriptable Object
    [SerializeField] private float _bulletSpeed;

    public static  event Action<Bullet> OnBoundaryReached;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 objectDirection)
    {
        _rigidbody2D.AddForce(objectDirection * _bulletSpeed);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponentInParent<Boundary>() || col.GetComponentInParent<Asteroid>())
        {
            OnBoundaryReached?.Invoke(this);
        }
    }
}
