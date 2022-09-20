using System;
using _Scripts.Projectiles;
using UnityEngine;

namespace _Scripts.FlyingObjects
{
    public class UFO : MonoBehaviour
    {
        private Player _player;
        public static event Action<UFO> OnUFODestroyed;
        public Player Player
        {
            get => _player;
            set => _player = value;
        }

//Too small for a seperate scriptable object 
        [SerializeField] private float moveSpeed;

        public void Construct(Player player)
        {
            _player = player;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position,
                moveSpeed * Time.deltaTime);
        }
        
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<Projectile>())
            {
                OnUFODestroyed?.Invoke(this);
            }
        }
    }
}