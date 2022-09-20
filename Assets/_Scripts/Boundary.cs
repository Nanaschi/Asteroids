using UnityEngine;

namespace _Scripts
{
    public class Boundary : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _leftBoxCollider2D;
        [SerializeField] private BoxCollider2D _UpBoxCollider2D;
        [SerializeField] private BoxCollider2D _RightBoxCollider2D;
        [SerializeField] private BoxCollider2D _downBoxCollider2D;
    
        public BoxCollider2D LeftBoxCollider2D => _leftBoxCollider2D;

        public BoxCollider2D UpBoxCollider2D => _UpBoxCollider2D;

        public BoxCollider2D RightBoxCollider2D => _RightBoxCollider2D;

        public BoxCollider2D DownBoxCollider2D => _downBoxCollider2D;

    }
}