using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Zomboy : MonoBehaviour
{
    public float lineOfSight;
    public float attackRate;

    public bool attacking;

    public Rigidbody2D rb;
    public Animator body;

    public AIPath path;
    private Transform player;
    public Transform attackPoint;
    public GameObject attackPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        Vector2 aimDir = (player.position - transform.position);
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        if (distanceFromPlayer < lineOfSight / 2)
        {
            path.enabled = false;
            attacking = true;
        }
        else
        {
            path.enabled = true;
            attacking = false;
        }

        if (!attacking)
        {
            return;
        }
        else
        {
            StartCoroutine(Attack());
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight - 45);
    }
    IEnumerator Attack()
    {
        attacking = true;

        Vector2 aimDir = (player.position - transform.position);
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        body.Play("DeadAttacking");
        //FindObjectOfType<AudioManager>().Play("Roar");
        Instantiate(attackPrefab, attackPoint.position, Quaternion.Euler(0, 0, angle));

        yield return new WaitForSeconds(attackRate);

        attacking = false;
    }
}
