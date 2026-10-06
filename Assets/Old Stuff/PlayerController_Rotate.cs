using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(InputManager))]
public class PlayerController_Rotate : MonoBehaviour
{
   [Header("Movement")] [Space(5)]
   public float moveSpeed;
   private Vector2 _lookDirection;
   
   [Header("Projectile Spawn")] [Space(5)]
   public GameObject projectile;
   public float projectileSpeed;
   public float spawnTime;
   private float _spawnTimeCounter;
   private Vector2 _spawnPosition;
   
   [Header("Components")]
   private InputManager _input;
   private Rigidbody2D _rigidbody2D;
   
   [Header("Collision")]
   private GameObject _currentlyInteractingWith;
   
   public Transform projectileSpawn;
   private Vector3 _mousePos;
   private Camera _camera;
   
   private void Start()
   {
      _camera = Camera.main;
      _input = GetComponent<InputManager>();
      _rigidbody2D = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      UpdateLookDirection();
      UpdateProjectileSpawn();
   }

  
   private void FixedUpdate()
   {
      _rigidbody2D.linearVelocity = _input.MoveDirection * moveSpeed;
   }
   
   private void UpdateProjectileSpawn()
   {
      if (!(Time.time > _spawnTimeCounter)) return;
            
      var projectileClone = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
      projectileClone.TryGetComponent(out Rigidbody2D rb2D);
      
      projectileClone.transform.right = transform.right.normalized;
      rb2D.linearVelocity = projectileClone.transform.right  * projectileSpeed;
      
      _spawnTimeCounter = Time.time + spawnTime;
      Destroy(projectileClone, 4f);
   }

   private void UpdateLookDirection()
   {
      var LookPos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
      var direction = LookPos - transform.position;
      
      var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0,0,angle);
      
      if (_input.MoveDirection.x != 0)
      {
         transform.localScale = new Vector3(Mathf.Sign(_input.MoveDirection.x), 1, 1);
      }
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