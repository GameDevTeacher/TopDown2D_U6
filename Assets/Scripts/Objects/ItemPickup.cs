using System;
using Interfaces;
using Player;
using UnityEngine;

namespace Objects
{
    public class ItemPickup : MonoBehaviour, IPickupable
    {
        public DictionaryAsset assetDic;
        
        public PickupType type;
        
        public void Pickup()
        {
            if (!PlayerPickupManager.pickupDictionary.TryAdd(assetDic.prefabDictionary[type], 1))
            {
                PlayerPickupManager.pickupDictionary[assetDic.prefabDictionary[type]] += 1;
            }

            Destroy(gameObject);
        }
    }
}