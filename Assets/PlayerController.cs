using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputManager))]
public class PlayerController : MonoBehaviour
{
   public float moveSpeed;
   private InputManager _input;
   private Rigidbody2D _rigidbody2D;

   private GameObject _currentlyInteractingWith;

   private void Start()
   {
      _input = GetComponent<InputManager>();
      _rigidbody2D = GetComponent<Rigidbody2D>();
   }

   private void FixedUpdate()
   {
      _rigidbody2D.linearVelocity = _input.moveDirection * moveSpeed;
   }

   private bool CanInteract()
   {
      var interactCollision = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Interactable"));
      _currentlyInteractingWith = interactCollision.gameObject;
      
      return interactCollision;
   }

   private void DirectionalAttack()
   {
      // Melee Attack (8 dir?)
      // Swipe with "Sword"
   }
}