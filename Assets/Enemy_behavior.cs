using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behavior : MonoBehaviour
{
    #region Public Variables
    public GameObject punch;
    public Transform rayCast;
    public LayerMask raycastMask;
    public Animator anim;
    public int punchPicker;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private float attackSpeed;
    private float distance;
    private bool punching;
    private bool blocking;
    private bool fighting;
    private bool inRange;
    private float intTimer;
    #endregion

    void Awake()
    {
        intTimer = timer;
    }

    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
            RaycastDebugger();
        }

        if (hit.collider != null)
        {
            inRange = true;
            EnemyLogic();
        } else if(hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            anim.SetBool("Blocking", false);
            blocking = false;
        }
    }
    
    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;
            blocking = true;
            Block();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance > attackDistance)
        {
            Block();
        } 
        else if (attackDistance < distance && blocking == true)
        {
            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        punchPicker = Random.Range(1, 6);
        anim.SetInteger("Punch", punchPicker);
        anim.SetTrigger("Punching");
        punching = true;
        punch.SetActive(true);

        yield return new WaitForSeconds(attackSpeed);
        punchPicker = 0;
        anim.SetInteger("Punch", punchPicker);
        punching = false;
        punch.SetActive(false);

        yield return new WaitForSeconds(attackSpeed/4);
        Block();

    }

    void Block()
    {
        blocking = true;
        punching = false;
        anim.SetBool("Punching", false);

    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        } 
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }
}
