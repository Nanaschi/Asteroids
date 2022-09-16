using UnityEngine;

namespace _Scripts
{
    public class ProjectileFactory
    {

        public static Bullet Projectile(Bullet bullet, Transform shooter)
        {
            return Object.Instantiate(bullet, shooter.position, shooter.rotation);
        }
    }
}