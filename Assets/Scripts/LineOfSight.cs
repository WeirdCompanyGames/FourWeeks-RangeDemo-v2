using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public float visionDistance;
    PistolEnemy body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, visionDistance);
        if(hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if (hitInfo.collider.tag == "Player")
            {
                if (!body.shooting && !body.reloading)
                {
                    StartCoroutine(Shoot());
                }   
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.up * visionDistance, Color.green);
        }
    }

   
    IEnumerator Shoot()
    {
        body.shooting = true;

        Vector2 aimDir = (body.player.position - transform.position);
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        body.rb.rotation = angle;

        body.body.Play("pistolshoot");
        FindObjectOfType<AudioManager>().Play("9mm");
        Instantiate(body.bulletPrefab, body.barrelPoint.position, Quaternion.Euler(0, 0, angle - (Random.Range(body.minRecoil, body.maxRecoil))));

        yield return new WaitForSeconds(body.fireRate);

        body.roundsLeft--;
        body.shooting = false;
    }
}
