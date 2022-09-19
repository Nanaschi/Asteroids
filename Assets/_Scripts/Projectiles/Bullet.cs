using System;
using UnityEngine;

public class Bullet : Projectile
{
    
    public static event Action<Bullet> OnBoundaryReached;
    
    public override void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.GetComponentInParent<Boundary>() || col.GetComponent<Asteroid>())
        {
            OnBoundaryReached?.Invoke(this);
        }
    }
}