using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;
        public static bool CanMove;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
        }
        
        public void UpdateMovement(Vector2 moveDirection)
        {
            if (!CanMove)
            {
                _rigidbody2D.linearVelocity = Vector2.zero;
                return;
            }
            
            _rigidbody2D.linearVelocity = moveDirection * _moveSpeed;
            
            if (moveDirection.x != 0)
            {
                _transform.localScale = new Vector2(Mathf.Sign(moveDirection.x), _transform.localScale.y);
            }
        }
    }
}