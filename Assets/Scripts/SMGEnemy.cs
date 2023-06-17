using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SMGEnemy : MonoBehaviour
{
    public float lineOfSight;
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
    private Transform player;
    public Transform barrelPoint;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        body.Play("SMGidle");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        roundsLeft = magCapacity;
    }

    // Update is called once per frame
    void Update()
    {

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        Vector2 aimDir = (player.position - transform.position);
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        if (distanceFromPlayer < lineOfSight / 1.25f)
        {
            body.SetBool("Aiming", true);
            path.enabled = false;
            aiming = true;
        }
        else
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
            else
            {
                if (!shooting && !reloading)
                {
                    StartCoroutine(Shoot());
                }
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
    IEnumerator Shoot()
    {
        shooting = true;

        Vector2 aimDir = (player.position - transform.position);
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        body.Play("SMGshoot");
        FindObjectOfType<AudioManager>().Play("9mm");
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(minRecoil, maxRecoil))));

        yield return new WaitForSeconds(fireRate);

        roundsLeft--;
        shooting = false;
    }

    IEnumerator Reload()
    {
        reloading = true;

        body.Play("MP5reload");
        FindObjectOfType<AudioManager>().Play("9mmReload");

        yield return new WaitForSeconds(reloadSpeed);

        roundsLeft = magCapacity;
        reloading = false;

    }
}
