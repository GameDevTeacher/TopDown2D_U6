namespace UnityEngine
{
    public class EnemyHealthManager : MonoBehaviour, IDamageable<float>
    {
        public float enemyHealth;
        
        public void Damage(float damageTaken, DamageTypes types)
        {
            enemyHealth -= damageTaken;
            if (enemyHealth<= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}