using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed = 10f;
    public int damage = 52;
    //public GameObject hitEffect;
    public Rigidbody2D rb;
    public BoxCollider2D bullet;

    public LayerMask hitLayer;
    public Transform hitCheck;
    public bool isHit;
    public float hitRadius;

    public float minRange;
    public float maxRange;


    void Awake()
    {
        minRange = 0.5f;
        maxRange = 0.8f;
        //Launch that fucker
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        Destroy(gameObject, Random.Range(minRange, maxRange));
    }
    public void FixedUpdate()
    {
        isHit = Physics2D.OverlapCircle(hitCheck.position, hitRadius, hitLayer);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        Health ally = collision.GetComponent<Health>();
        PlayerHealth target = collision.GetComponent<PlayerHealth>();
        BoxCollider2D collider = collision.GetComponent<BoxCollider2D>();

        if (target != null)
        {
            this.enabled = true;
            target.TakeDamage(damage);
        }
        else if (collider != null || ally != null)
        {
            this.gameObject.SetActive(false);
        }
        else if (ally != null)
        {
            this.gameObject.SetActive(false);
        }

        DestroyIt();
    }

    void DestroyIt()
    {
        Destroy(this, 0.001f);
        //Instantiate(hitEffect, transform.position, transform.rotation);
    }
}
