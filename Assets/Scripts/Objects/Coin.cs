using Interfaces;
using UnityEngine;

namespace Objects
{
    public class Coin : MonoBehaviour, IPickupable
    {
        public GameObject GameObject => gameObject;

        public void Pickup()
        {
            Destroy(gameObject);
        }
    }
}