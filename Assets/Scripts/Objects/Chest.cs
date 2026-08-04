using UnityEngine;

namespace Objects
{
    public class Chest : MonoBehaviour , IInteractable, IDamageable<float>
    {
        public float health;
        public GameObject[] spawnables;
        
        private void Open()
        {
            print("The Chest opens");
        }
        
        public void Interact()
        {
            Open();
        }

        public void Damage(float damageTaken)
        {
            health -= damageTaken;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            
        }

        void Spawnables()
        {
            if (spawnables != null)
            {
                Instantiate(spawnables[Random.Range(0, spawnables.Length)], transform.position, Quaternion.identity);
            }
        }
    }
}