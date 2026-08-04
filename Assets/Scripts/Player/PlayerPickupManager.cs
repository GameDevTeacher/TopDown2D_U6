using Interfaces;
using UnityEngine;
using UnityEngine.Rendering;

namespace Player
{
    public class PlayerPickupManager : MonoBehaviour
    {
        public SerializedDictionary<GameObject, int> pickupDictionary = new SerializedDictionary<GameObject, int>();
        
        [SerializeField] public SerializedDictionary<GameObject, int> visibleDic;

        private void Update()
        {
            if (Time.frameCount % 30 == 0)
            { 
                visibleDic = pickupDictionary;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IPickupable pickupable))
            {
                pickupable.Pickup();
            }
        }
    }
}