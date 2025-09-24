using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputManager))]
public class PlayerController : MonoBehaviour
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
   
   private void Start()
   {
      _input = GetComponent<InputManager>();
      _rigidbody2D = GetComponent<Rigidbody2D>();

      if (_lookDirection == Vector2.zero)
      {
         _lookDirection.x = 0;
         _lookDirection.y = -1;
      }

      UpdateSpawnPosition();
   }

   private void Update()
   {
      UpdateLookDirection();
      UpdateProjectileSpawn();
   }

  
   private void FixedUpdate()
   {
      _rigidbody2D.linearVelocity = _input.moveDirection * moveSpeed;
   }
   
   private void UpdateProjectileSpawn()
   {
      if (!(Time.time > _spawnTimeCounter)) return;
            
      var projectileClone = Instantiate(projectile, UpdateSpawnPosition(), Quaternion.identity);
      
      projectileClone.TryGetComponent(out Rigidbody2D rb2D);
      rb2D.linearVelocity = _lookDirection * projectileSpeed + _rigidbody2D.linearVelocity;
      
      // Get the angle between Y and X // Turns the Radians to Degrees
      var angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
      rb2D.transform.rotation = Quaternion.Euler(0, 0, angle);
 
      /* This only works for when you are Rotating the Player
      projectileClone.transform.right = transform.right.normalized;
      rb2D.linearVelocity = projectileClone.transform.right * projectileSpeed;
      */
      
      _spawnTimeCounter = Time.time + spawnTime;
      Destroy(projectileClone, 4f);
   }

   private Vector2 UpdateSpawnPosition()
   {
      _spawnPosition.x = transform.localPosition.x + (_input.moveDirection.x/2);
      _spawnPosition.y = transform.localPosition.y + (_input.moveDirection.y/2);

      if (_spawnPosition == Vector2.zero)
      {
         _spawnPosition.x = transform.localPosition.x + 0.5f;
      }
      return _spawnPosition;
   }

   private void UpdateLookDirection()
   {
      if (_input.moveDirection != Vector2.zero)
      {
         _lookDirection = _input.moveDirection;
      }
      
      if (_lookDirection.x != 0)
      {
         transform.localScale = new Vector3(Mathf.Sign(_lookDirection.x), 1, 1);
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