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
            _animation.UpdateAnimation_New(_input.moveDirection, _input.attackPressed);
            _attack.UpdateAttack(_input.attackPressed, _input.lookDirection);
        }

        private void FixedUpdate()
        {
            _movement.UpdateMovement(_input.moveDirection);
        }
    }
}