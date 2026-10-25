using UnityEngine;

public abstract class EnemyBase: MonoBehaviour, IDamage
{

    [SerializeField] BulletType Weakness;
    [SerializeField] float moveSpeed;
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet Bullet = collision.gameObject.GetComponent<Bullet>();
        if (Weakness == Bullet.BulletForm)
        {
            Destroy(gameObject);
        }

    }

    public void TakeDamage()
    {
        {
            Destroy(gameObject);
        }
    }
}
