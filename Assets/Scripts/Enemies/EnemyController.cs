using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 playerDirection;
    [SerializeField] private Transform playerTransform;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Time.frameCount % 30 == 0)
        {
            playerDirection = playerTransform.position - transform.position;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = playerDirection * moveSpeed;
    }
}
