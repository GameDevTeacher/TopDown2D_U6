using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public GameObject projectile;
        public float projectileSpeed;
        public float spawnTime;
        private float _spawnTimeCounter;
        private Vector2 _spawnPosition;
    
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void UpdateAttack(bool attackPressed, Vector2 moveDirection)
        {
            if (!(Time.time > _spawnTimeCounter)) return;
            if (!attackPressed) return;
            StartCoroutine(FireDelay(moveDirection));
            
            return;

            IEnumerator FireDelay(Vector2 direction)
            {
                yield return new WaitForSeconds(0.17f);
                
                var projectileClone = Instantiate(projectile, UpdateSpawnPosition(), Quaternion.identity);
      
                projectileClone.TryGetComponent(out Rigidbody2D rb2D);
                rb2D.linearVelocity = direction * projectileSpeed;
      
                // Get the angle between Y and X // Turns the Radians to Degrees
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb2D.transform.rotation = Quaternion.Euler(0, 0, angle);
            
                _spawnTimeCounter = Time.time + spawnTime;
                Destroy(projectileClone, 4f);
            }
            
            Vector2 UpdateSpawnPosition()
            {
                _spawnPosition.x = _transform.localPosition.x + (moveDirection.x / 2);
                _spawnPosition.y = _transform.localPosition.y + (moveDirection.y / 2);

                if (_spawnPosition == Vector2.zero)
                {
                    _spawnPosition.x = transform.localPosition.x + 0.5f;
                }
                
                return _spawnPosition;
            }
        }
    }
}