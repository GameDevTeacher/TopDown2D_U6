using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private float damageAnimTime;
        private float _lockedTill;
        private int _currenState;
        
        private Animator _animator;
        
        private void Awake() => _animator = GetComponent<Animator>();

        public void UpdateAnimation_New(Vector2 moveDirection, bool attackPressed)
        {
            if (moveDirection != Vector2.zero)
            {
                _animator.SetFloat(X, moveDirection.x);
                _animator.SetFloat(Y, moveDirection.y);
            }
            
            var state = GetState();

            PlayerMovement.CanMove = state != Attack;
            
            if (state == _currenState) return;
            
            _animator.CrossFade(state, 0,0);
            _currenState = state;

            return;
            
            int GetState()
            {
                if (Time.time < _lockedTill) return _currenState;

                if (attackPressed) return LockState(Attack, damageAnimTime);
                return moveDirection != Vector2.zero ? Move : Idle;

                int LockState(int s, float t)
                {
                    _lockedTill = Time.time + t;
                    return s;
                }
            }
        }
        

        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");
        
        public void UpdateAnimation_Old(Vector2 moveDirection, bool attackPressed)
        {
            if (moveDirection != Vector2.zero)
            {
                _animator.SetFloat(X, moveDirection.x);
                _animator.SetFloat(Y, moveDirection.y);
            }

            _animator.Play(moveDirection != Vector2.zero ? Move : Idle);
        }
    }
}