using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]

    public abstract class Projectile : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        //too small for a separate Scriptable Object
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
}