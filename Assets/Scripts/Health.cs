using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Damage Logic")]
    public LayerMask bullet;
    public Rigidbody2D rb;

    public Transform hitPoint;

    [Header("Misc")]
    public int health = 1;
    private int maxHealth;

    public bool isDead = false;

    //public GameObject deathEffect;
    public void Start()
    {
        maxHealth = 100;
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }


    public void Death()
    {
        //Instantiate(deathEffect, hitPoint.position, hitPoint.rotation);

        rb.isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.SetActive(false);
        return;

    }
}
