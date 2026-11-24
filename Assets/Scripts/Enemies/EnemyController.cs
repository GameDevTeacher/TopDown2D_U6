using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 playerDirection;
    public float distance;
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
            distance = Vector2.Distance(transform.position, playerTransform.position);
        }

        var hit = Physics2D.Raycast(transform.position, playerDirection, Mathf.Infinity);

        if (!hit) return;

        if (hit.transform.CompareTag("Player"))
        {
            print("myname: " + this.name +" "+ "your name: " + hit.transform.name);
        }
    }

    private void OnDrawGizmos()
    {
        UnityEngine.Debug.DrawRay(transform.position, playerDirection, Color.red);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = playerDirection * moveSpeed;
    }
}
