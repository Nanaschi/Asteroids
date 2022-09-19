using UnityEngine;

class Bullet : Projectile
{
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponentInParent<Boundary>() || col.GetComponent<Asteroid>())
        {
            BoundaryReached(this);
        }
    }
}