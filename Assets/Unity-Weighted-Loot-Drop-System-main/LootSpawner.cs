//3. LootSpawner.cs

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

/// Attach this to an Enemy, Chest, or destructible barrel.

public class LootSpawner : MonoBehaviour
{
    [Header("Loot Settings")]
    public LootTable myLootTable;
    
    [Tooltip("Where should the item spawn? Leave empty to spawn at this object's center.")]
    public Transform spawnPoint;

    [Tooltip("How much random force to apply to the dropped item (makes loot 'pop' out).")]
    public float dropForce = 3f;

    private void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            DropLoot();
        }
    }

    public void DropLoot()
    {
        if (myLootTable == null)
        {
            UnityEngine.Debug.LogWarning("No Loot Table assigned to " + gameObject.name);
            return;
        }

        // Ask the ScriptableObject to process the math and give us all winning items
        List<GameObject> itemsToSpawn = myLootTable.GenerateDrops();

        if (itemsToSpawn.Count > 0)
        {
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
            
            // Loop through our list and spawn every item (Guaranteed + Weighted)
            foreach (GameObject itemPrefab in itemsToSpawn)
            {
                GameObject spawnedLoot = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

                // Optional: Add a little physics "pop" so the loot flies out
                
                if (spawnedLoot.TryGetComponent(out Rigidbody2D rb))
                {
                    Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                    rb.AddForce(randomDirection * dropForce, ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            UnityEngine.Debug.Log(gameObject.name + " dropped nothing.");
        }
    }
}
