using System;
using UnityEngine;

namespace _Scripts.Projectiles
{
    public class Laser : Projectile
    {
        public static event Action<Laser> OnBoundaryReached;
    
        public override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponentInParent<Boundary>())
            {
                OnBoundaryReached?.Invoke(this);
            }
        }
    }
}