using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ShotgunEnemy : MonoBehaviour
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
        body.Play("shotgunidle");
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
        if (distanceFromPlayer < lineOfSight / 1.5f) 
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

        body.Play("shogtunshoot");
        FindObjectOfType<AudioManager>().Play("Shotgun");
        FindObjectOfType<AudioManager>().Play("Shotgun2");
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(minRecoil, maxRecoil))));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(minRecoil, maxRecoil))));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(minRecoil, maxRecoil))));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(minRecoil, maxRecoil))));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(minRecoil, maxRecoil))));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(minRecoil, maxRecoil))));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(minRecoil, maxRecoil))));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(minRecoil, maxRecoil))));
        roundsLeft--;

        yield return new WaitForSeconds(reloadSpeed / 2);
        FindObjectOfType<AudioManager>().Play("ShotgunRack");

        body.Play("cockshotgun");

        yield return new WaitForSeconds(fireRate);

        shooting = false;
    }

    IEnumerator Reload()
    {
        reloading = true;
        if (roundsLeft <= magCapacity)
        {
            yield return new WaitForSeconds(reloadSpeed);
            FindObjectOfType<AudioManager>().Play("ShotgunShell");
            roundsLeft = magCapacity;
            Animator player = FindObjectOfType<PlayerMovement>().anim;
            body.Play("shotgunreload");
            yield return new WaitForSeconds(reloadSpeed / 2);
            FindObjectOfType<AudioManager>().Play("ShotgunRack");
            reloading = false;
        }
    }
}
