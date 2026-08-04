using Interfaces;
using Player;
using UnityEngine;

namespace Objects
{
    public class ItemPickup : MonoBehaviour, IPickupable
    {
        public GameObject item;

        public PlayerPickupManager pickupManager;

        //public GameObject GameObject { get; }

        public void Pickup()
        {
            if (!pickupManager.pickupDictionary.ContainsKey(item))
            {
                pickupManager.pickupDictionary.Add(item, 1);
            }
            else
            {
                pickupManager.pickupDictionary[item] += 1;
            }
            Destroy(gameObject);
        }
    }
}