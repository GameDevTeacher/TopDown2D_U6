using UnityEngine;

namespace Objects
{
    public class ObjectHealthManager : MonoBehaviour, IDamageable<float>
    {
        public float objectHealth;
        public void Damage(float damageTaken)
        {
            objectHealth -= damageTaken;
        }
    }
}