using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private float speed;
    private float damage;

    public void StartFly(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
        Vector2 direction = new Vector2(transform.right.x, transform.right.y);
        rb.AddForce(direction * speed);
        Invoke("DestroyBullet", 3f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.tag);
        if (col.tag == "EnemyHitBox")
        {
            col.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
        }
        DestroyBullet();
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
