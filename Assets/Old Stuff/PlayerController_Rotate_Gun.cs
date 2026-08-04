using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(InputManager))]
public class PlayerController_Rotate_Gun : MonoBehaviour
{
   [Header("Movement")] [Space(5)]
   public float moveSpeed;
   private Vector2 _lookDirection;
   public Transform gunTransform;
   
   [Header("Projectile Spawn")] [Space(5)]
   public GameObject projectile;
   public float projectileSpeed;
   public float spawnTime;
   private float _spawnTimeCounter;
   private Vector2 _spawnPosition;
   
   [Header("Components")]
   private InputManager _input;
   private Rigidbody2D _rigidbody2D;
   private SpriteRenderer _spriteRenderer;
   
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
      _spriteRenderer = GetComponent<SpriteRenderer>();
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
      gunTransform.rotation = Quaternion.Euler(0,0,angle);
      
      if (_input.moveDirection.x != 0)
      {
         //transform.localScale = new Vector3(Mathf.Sign(_input.moveDirection.x), 1, 1);
         //_spriteRenderer.flipX = _input.moveDirection.x < 0;
      }
   }

   private bool _isFacingRight = false;

   private void UpdateGunRotation()
   {
      var gunLookPos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - gunTransform.position;
      var gunRotation = Mathf.Atan2(gunLookPos.y, gunLookPos.x)*Mathf.Rad2Deg;

      if (_isFacingRight)
      {
         if (gunRotation < 115f && gunRotation > -115f)
         {
            
         }
      }
      /*
      weaponDistance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weapon.transform.position;
      weaponRotation = Mathf.Atan2(weaponDistance.y, weaponDistance.x) * Mathf.Rad2Deg;

      if (isFacingRight)
      {
         if (weaponRotation < 115f && weaponRotation > -115f)
         {
            weapon.transform.localPosition = rightWeaponPosition;
            weapon.transform.localRotation = Quaternion.Euler(0f, 0f, weaponRotation);
         }
         else
         {
            if (Vector2.Distance(transform.localPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.8f)
               weapon.transform.localRotation = Quaternion.Euler(0f, 0f, weaponRotation);
            else
               isFacingRight = false;
         }
      }
      else
      {
         if ((weaponRotation > 65f && weaponRotation < 180f) || (weaponRotation > -180f && weaponRotation < -65f))
         {
            weapon.transform.localPosition = leftWeaponPosition;
            weapon.transform.localRotation = Quaternion.Euler(180f, 0f, -weaponRotation);
         }
         else
         {
            if (Vector2.Distance(transform.localPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.8f)
               weapon.transform.localRotation = Quaternion.Euler(0f, 0f, weaponRotation);
            else
               isFacingRight = true;
         }
      }*/
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