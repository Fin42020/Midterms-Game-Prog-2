using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour,IDamage
{
    private Rigidbody2D Bullet_rb;
    public float BulletSpeed = 10f;
    public InputActionReference SwitchColor;
    public BulletType BulletForm;

    private void Awake()
    {
        Bullet_rb = GetComponent<Rigidbody2D>();
        Bullet_rb.linearVelocity = transform.right * BulletSpeed;
        StartCoroutine(KYSNow());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    #region KYSNow Method
    IEnumerator KYSNow()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
    #endregion
