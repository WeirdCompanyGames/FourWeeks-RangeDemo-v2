using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSMG : MonoBehaviour
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
    public float burstRate;
    public float nextTimeToFire = 0f;
    private bool isFiring = false;
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

        magCapacity = 30;
        burstRate = 20;
        roundsLeft = magCapacity;

        minRange = 0.25f;
        minRange = 0.75f;
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
                    //arms.gameObject.SetActive(true);
                    if (Input.GetButton("Fire") && Time.time >= nextTimeToFire)
                    {
                        if (isFiring)
                            return;
                        if (isReloading)
                            return;

                        if (roundsLeft <= 0 && isReloading == false)
                        {
                            roundsLeft = 0;
                            return;
                        }
                        else
                        {
                            nextTimeToFire = Time.time + 1f / fireRate;
                            Shoot();
                        }
                    }
                    else if (Input.GetButtonDown("Fire") && Time.time >= nextTimeToFire)
                    {
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
        void OnDisable()
        {
            Holster();
        }
        void Equip()
        {
            equipped = true;
            FindObjectOfType<PlayerStats>().equippedRifle = true;
            player.Play("SMGdraw");
        }
        void Holster()
        {
            equipped = false;
            FindObjectOfType<PlayerStats>().equippedRifle = false;
            player.Play("SMGholster");
        }

        void Shoot()
        {
            isFiring = true;

            //Play shoot animation
            Animator player = FindObjectOfType<PlayerMovement>().anim;
            player.Play("SMGshoot");

            GameObject bullet = Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, (transform.eulerAngles.z) + Random.Range(minRecoil, maxRecoil)));
            roundsLeft--;
            Destroy(bullet, 2f);
            StartCoroutine(ShootTimer());

            isFiring = false;
        }

        IEnumerator ShootTimer()
        {
            yield return new WaitForSeconds(fireRate);
        }
        IEnumerator Reload()
        {
            isReloading = true;

            yield return new WaitForSeconds(reloadSpeed);

            Animator player = FindObjectOfType<PlayerMovement>().anim;
            player.Play("MP5reload");
            FindObjectOfType<AudioManager>().Play("SMGReload");

            roundsLeft = magCapacity;
            isReloading = false;

        }
}
