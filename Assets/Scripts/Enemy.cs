using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Combat Logic")]
    public Rigidbody2D rb;
    public LayerMask bullet ;
    public Animator animator;
    public Transform hitPoint;

    [Header("Misc")]
    public int health = 1;
    public bool isDead = false;

    //public GameObject deathEffect;

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

        animator.SetBool("Dead", true);
        //Instantiate(deathEffect, hitPoint.position, hitPoint.rotation);

        rb.isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 0f);
        return;

    }
}
