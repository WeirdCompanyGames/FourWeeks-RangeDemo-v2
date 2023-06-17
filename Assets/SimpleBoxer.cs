using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBoxer : MonoBehaviour
{
    public float lineOfSight;

    public float punchDelay;
    public int punchDamage;
    public int punchPicker;
    public bool fighting;
    public bool blocking;
    public bool punching;

    public Rigidbody2D rb;
    public Animator body;
    private Transform player;
    public Transform barrelPoint;

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

        if (distanceFromPlayer < lineOfSight)
        {
            body.SetBool("Fighting", true);
            fighting = true;
        }
        else
        {
            body.SetBool("Fighting", false);
            fighting = false;
        }

        if (fighting)
        {
            if (distanceFromPlayer < lineOfSight / 2)
            {
                if (!punching)
                    StartCoroutine(Block());
            }
            else if (distanceFromPlayer < lineOfSight / 3)
            {
                StartCoroutine(Punch());
            }
        }
        else
        {

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
    IEnumerator Punch()
    {
        punching = true;
        punchPicker = Random.Range(1, 6);
        body.SetInteger("Punch", punchPicker);
        body.SetTrigger("Punching");

        Vector2 aimDir = (player.position - transform.position);
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        yield return new WaitForSeconds(punchDelay);

        punching = false;
    }

    IEnumerator Block()
    {
        blocking = true;
        body.SetBool("Blocking", true);

        yield return new WaitForSeconds(punchDelay);

        blocking = false;
        body.SetBool("Blocking", false);

    }

    
}
