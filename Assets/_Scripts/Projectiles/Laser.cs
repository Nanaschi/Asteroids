using UnityEngine;

class Laser : Projectile
{
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponentInParent<Boundary>())
        {
            BoundaryReached(this);
        }
    }
}