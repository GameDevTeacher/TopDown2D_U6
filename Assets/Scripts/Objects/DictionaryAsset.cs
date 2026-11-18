using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    [CreateAssetMenu]
    public class DictionaryAsset : ScriptableObject
    {
        [SerializeField] public Dictionary<PickupType, GameObject> prefabDictionary;
    }
    public enum PickupType
    {
        Gem,
        Coin,
        Feathers,
    }
}