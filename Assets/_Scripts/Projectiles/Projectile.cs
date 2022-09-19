using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
//TODO: make it a base for bullet and laser
//the difference will be in if (col.GetComponentInParent<Boundary>() || col.GetComponent<Asteroid>())
//also in _bulletSpeed probably in Scriptable Objects
public abstract class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    //TODO: too small for a separate Scriptable Object
    [FormerlySerializedAs("_bulletSpeed")] [SerializeField]
    private float _projectileSpeed;

   


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 objectDirection)
    {
        _rigidbody2D.AddForce(objectDirection * _projectileSpeed);
    }

    public abstract void OnTriggerEnter2D(Collider2D col);
    
    
}