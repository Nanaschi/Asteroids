using UnityEngine;

namespace _Scripts
{
    public class ProjectileFactory: PoolerBase<Bullet>
    {
        public static Bullet Projectile(Bullet bullet, Transform shooter)
        {
           return Instantiate(bullet, shooter.position, shooter.rotation);
        }
    }
}