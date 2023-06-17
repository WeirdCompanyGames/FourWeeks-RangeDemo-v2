using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShotgun : MonoBehaviour
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
        
        magCapacity = 10 ;
        roundsLeft = magCapacity;

        minRange = 0.35f;
        minRange = 0.45f;

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
                        StartCoroutine(Shoot());
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
        FindObjectOfType<PlayerStats>().equippedShotgun = true;
        player.Play("shotgundraw");
    }
    void Holster()
    {
        equipped = false;
        FindObjectOfType<PlayerStats>().equippedShotgun = false;
        player.Play("shotgunholster");
    }


    IEnumerator Shoot()
    {
        Animator player = FindObjectOfType<PlayerMovement>().anim;


        //Play shoot animation

        player.Play("shotgunshoot");
        //Make bullet at barrel
        GameObject bullet = Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(1, 2)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minRecoil, maxRecoil * 1.5f)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minRecoil, maxRecoil * 2f)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - Random.Range(minRecoil, maxRecoil) / 1.5f));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - Random.Range(minRecoil, maxRecoil) / 2f)); 
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(1, 2)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minRecoil, maxRecoil * 1.5f)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minRecoil, maxRecoil * 2f)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - Random.Range(minRecoil, maxRecoil) / 1.5f));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - Random.Range(minRecoil, maxRecoil) / 2f));
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(1, 2)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minRecoil, maxRecoil * 1.5f)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minRecoil, maxRecoil * 2f)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - Random.Range(minRecoil, maxRecoil) / 1.5f));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - Random.Range(minRecoil, maxRecoil) / 2f)); 
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, barrelPoint.rotation);
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(1, 2)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minRecoil, maxRecoil * 1.5f)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(minRecoil, maxRecoil * 2f)));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - Random.Range(minRecoil, maxRecoil) / 1.5f));
        Instantiate(bulletPrefab, barrelPoint.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - Random.Range(minRecoil, maxRecoil) / 2f));
        //Lose a shell
        roundsLeft--;

        //Rerack
        yield return new WaitForSeconds(reloadSpeed / 2);
        FindObjectOfType<AudioManager>().Play("ShotgunRack");

        player.Play("shotguncock");

        //Let it die
        Destroy(bullet, 2f);
    }
    IEnumerator Reload()
    { 
        isReloading = true;
        if (roundsLeft <= magCapacity)
        {
            yield return new WaitForSeconds(reloadSpeed);
            FindObjectOfType<AudioManager>().Play("ShotgunShell");
            roundsLeft = magCapacity;
            Animator player = FindObjectOfType<PlayerMovement>().anim;
            player.Play("shotgunreload");
            yield return new WaitForSeconds(reloadSpeed/2);
            FindObjectOfType<AudioManager>().Play("ShotgunRack");
            isReloading = false;
        }


    }
}
