using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Damage Logic")]
    public LayerMask bullet;
    public Rigidbody2D rb;

    public Animator animator;
    public Transform hitPoint;

    [Header("Misc")]
    public int health = 100;
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
        if (health <= 0)
        {
            health = 0;
            Death();
        }

        health -= damage;
    }


    public void Death()
    {
        Cursor.visible = true;
        Debug.Log("You died. Game restarted.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        return;

    }
}
