using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerAnimation), typeof(PlayerAttack))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _movement;
        private PlayerAnimation _animation;
        private PlayerAttack _attack;
        private InputManager _input;

        private void Start()
        {
            _movement = GetComponent<PlayerMovement>();
            _animation = GetComponent<PlayerAnimation>();
            _attack = GetComponent<PlayerAttack>();
            _input = GetComponent<InputManager>();
        }

        private void Update()
        {
            _animation.UpdateAnimation_New(_input.MoveDirection, _input.AttackPressed);
            _attack.UpdateAttack(_input.AttackPressed, _input.LookDirection);
        }

        private void FixedUpdate()
        {
            _movement.UpdateMovement(_input.MoveDirection);
        }
    }
}