using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PistolEnemy : MonoBehaviour
{
    public float lineOfSight;
    public float retreatDistance;

    public float fireRate;
    public float reloadSpeed;
    public float roundsLeft;
    public float magCapacity;
    public float minRecoil;
    public float maxRecoil;

    public bool aiming;
    public bool shooting;
    public bool reloading;

    public Rigidbody2D rb;
    public Animator body;
    public PolygonCollider2D sightPath;

    public AIPath path;
    public Transform player;
    private Transform ally;
    public Transform barrelPoint;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ally = GameObject.FindGameObjectWithTag("Enemy").transform;

        path.enabled = false;
        roundsLeft = magCapacity;
    }

    // Update is called once per frame
    void Update()
    {

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        float distanceFromAlly = Vector2.Distance(ally.position, transform.position);
        Vector2 aimDir = (player.position - transform.position);
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        if (distanceFromAlly < lineOfSight / 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, ally.position, -4f * Time.deltaTime);
            body.SetBool("Aiming", false);
            aiming = false;
        } else
        {
            body.SetBool("Aiming", false);
            aiming = false;
        }

        if (distanceFromPlayer < lineOfSight / retreatDistance)
        {
            body.SetBool("Aiming", true);
            path.enabled = false;
            aiming = true;
            transform.position = Vector2.MoveTowards(transform.position, player.position, -4f * Time.deltaTime);
        }
        else if (distanceFromPlayer < lineOfSight)
        {
            body.SetBool("Aiming", true);
            path.enabled = true;
            aiming = true;
        }
        else if (distanceFromPlayer > lineOfSight)
        {
            body.SetBool("Aiming", false);
            path.enabled = true;
            aiming = false;
        }

        if (aiming)
        {
            if (roundsLeft <= 0 && !reloading)
            {
                roundsLeft = 0;
                if (!reloading)
                    StartCoroutine(Reload());
            }
        }
        else
        {
            return;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSight / retreatDistance);
    }
   
    IEnumerator Reload()
    {
        reloading = true;

        body.Play("pistolemptyreload");
        FindObjectOfType<AudioManager>().Play("9mmReload");

        yield return new WaitForSeconds(reloadSpeed);

        roundsLeft = magCapacity;
        reloading = false;

    }


}
