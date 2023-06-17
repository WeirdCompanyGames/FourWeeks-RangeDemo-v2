using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour
{
    [Header("COMPONENTS")]
    public Animator animator;
    public BoxCollider2D hitBox;
    //public GameObject hitEffect;

    public PlayerStats player;
    public RangeManager range;

    [Space]
    [Header("STATS")]
    public int health = 100;

    public void Start()
    {
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("You hit for " + damage + ".");
        health -= damage;

        if (health <= 0)
        {
        
            health = 0;
            Break();
        }
        else
        {
            animator.Play("targetShot");
        }
    }

    public void Break()
    {
        //Disabled hit box
        Debug.Log("KILLED " + name +".");

        //player.enemiesKilled++;
        //range.roundKills++;
        //range.totalKills++;

        //animator.Play("targetBreak");
        this.gameObject.SetActive(false);
    }

    public void Reset()
    {
        
        health = 100;
        animator.Play("targetBlank");
        //Instantiate(hitEffect, hitPoint.position, hitPoint.rotation);

    }
}
