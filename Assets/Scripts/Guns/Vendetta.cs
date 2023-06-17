using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendetta : MonoBehaviour
{

    [Header("Reference Points")]
    public Animator player;
    public Transform barrelPoint;
    public GameObject bulletPrefab;
    public GameObject arms;
    public bool equipped;
    public bool aiming = false;

    [Space]
    [Header("Fire Rate")]
    public float fireRate = 1f;
    public float nextTimeToFire = 0f;
    [Space]
    [Header("Recoil")]
    public float minRecoil;
    public float maxRecoil;
    [Space]
    [Header("Capacity")]
    public float roundsLeft;
    public float magCapacity;
    [Space]
    [Header("Reload Speed")]
    public float reloadSpeed;
    private bool isReloading = false;
    [Space]
    [Header("Range")]
    public float minRange;
    public float maxRange;



    // Start is called before the first frame update
    void Awake()
    {
        magCapacity = 15;
        roundsLeft = magCapacity;

        minRange = 0.25f;
        minRange = 0.75f;

        bulletPrefab.GetComponent<Bullet>().minRange = bulletPrefab.GetComponent<Bullet>().minRange + minRange;
        bulletPrefab.GetComponent<Bullet>().maxRange = bulletPrefab.GetComponent<Bullet>().maxRange + maxRange;
    }

    void Update()
    {
        if (Input.GetButtonDown("Aim") && equipped == false && Time.time >= nextTimeToFire)
            Equip();
        else if (Input.GetButtonDown("Equip") && equipped == true && Time.time >= nextTimeToFire)
            Holster();

        if (equipped)
        {
            if (Input.GetButton("Aim"))
            {
                aiming = true;
                player.SetBool("Aiming", true);
                arms.SetActive(true);   

            }
            else
            {
                aiming = false;
                player.SetBool("Aiming", false);
                arms.SetActive(false);
            }
            if (Input.GetButtonDown("Reload"))
            {
                if (roundsLeft >= magCapacity)
                {
                    roundsLeft = magCapacity;
                    return;
                }
                nextTimeToFire = Time.time + 1f / reloadSpeed;
                StartCoroutine(Reload());
            }

            if (aiming)
            {
                arms.gameObject.SetActive(true);
                if (Input.GetButtonDown("Fire") && Time.time >= nextTimeToFire)
                {
                    if (isReloading)
                        return;

                    if (roundsLeft <= 0 && isReloading == false)
                    {
                        roundsLeft = 0;
                        FindObjectOfType<AudioManager>().Play("DryFire");
                        return;
                    }
                    else
                    {
                        nextTimeToFire = Time.time + 1f / fireRate;
                        Shoot();
                    }
                }
            }
        }
    }

    void Equip()
    {
        equipped = true;
        FindObjectOfType<PlayerStats>().equippedPistol = true;
        player.Play("pistoldraw");
    }
    void Holster()
    {
        equipped = false;
        FindObjectOfType<PlayerStats>().equippedPistol = false;
        player.Play("pistolholster");
    }

    void Shoot()
    {


        //Play fire sound
        FindObjectOfType<AudioManager>().Play("9mm");
        Animator player = FindObjectOfType<PlayerMovement>().anim;
        //Play shoot animation
        player.Play("pistolshoot");

        //Make bullet at barrel
        GameObject bullet = Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minRecoil, maxRecoil)));
        //Lose a round
        roundsLeft--;
        //Let it die
        Destroy(bullet, 2f);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadSpeed);

        Animator player = FindObjectOfType<PlayerMovement>().anim;
        player.Play("pistolreload");
        FindObjectOfType<AudioManager>().Play("9mmReload");

        roundsLeft = magCapacity;
        isReloading = false;
    }
    void OnDisable()
    {
        Holster();
    }
}
