using UnityEngine;

namespace Objects
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Scrubs/Tools/Weapon")]
    public class WeaponScrub : ScriptableObject
    {
        public new string name;
        public float damage;
        public float range;
        public DamageType damageType;
        public Sprite icon;
        public GameObject prefab;

        public enum DamageType
        {
            None,
            Poison,
            Electric,
            Magic,
            Cursed
        }
    }
}