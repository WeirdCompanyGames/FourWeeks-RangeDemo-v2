using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float speed = 10f;
    public int damage = 52;
    //public GameObject hitEffect;
    public Rigidbody2D rb;

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

        //Play fire sound
        FindObjectOfType<AudioManager>().Play("9mm");
    }
    public void FixedUpdate()
    {
        isHit = Physics2D.OverlapCircle(hitCheck.position, hitRadius, hitLayer);
        Destroy(this.gameObject, Random.Range(minRange, maxRange));
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        Health target = collision.GetComponent<Health>();
        BoxCollider2D collider = collision.GetComponent<BoxCollider2D>();

        if (collider.tag == "Enemy")
        {
            target.TakeDamage(damage);
            DestroyIt();
        }
        else if (collider.tag == "Walls")
        {
            DestroyIt();
        }
        DestroyIt();

       

        void DestroyIt()
        {
            Destroy(this.gameObject, 0.001f);
            //Instantiate(hitEffect, transform.position, transform.rotation);  
        }
    }
}
