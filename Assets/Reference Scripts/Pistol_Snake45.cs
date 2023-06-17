using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol_Snake45 : MonoBehaviour
{

    [Header("Reference Points")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    public Animator player;
    //public Animator UI;

    [Space]
    [Header("UI Elements (coming soon)")]

    [Space]
    [Header("Firearm Variables")]
    public int currentPistolID = 1;
    public int magSize = 12;
    public int magRounds;

    public float fireRate = 1f;
    public float nextTimeToFire = 0f;

    public float reloadRate = 3f;
    public float nextTimeToReload = 0f;

    public bool holstered;
    public bool reloading;


    // Start is called before the first frame update
    public void Awake()
    {
        //UI.SetBool("Show", false);

        player.SetLayerWeight(0, -1);
        player.SetLayerWeight(1, +1);
        holstered = true;
        Equip();
        MagReset();
    }

    void Update()
    {

        if (holstered == false && Input.GetKeyDown(KeyCode.H) || Input.GetButtonDown("Fire1"))
        {
            holstered = true;
            Holster();
        }
        else if (holstered == true && Input.GetKeyDown(KeyCode.H) || Input.GetButtonDown("Fire1"))
        {
            holstered = false;
            Equip();
        }

        if (reloading == false && Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && magRounds > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (reloading == false && Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && magRounds <= 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            //Execute empty mag function
            EmptyMag();
            player.SetTrigger("Shoot");
            //UI.SetTrigger("Empty");
        }



        if (Input.GetKeyDown(KeyCode.R) && Time.time >= nextTimeToFire)
        {
            nextTimeToReload = Time.time + 1f / reloadRate;
            ReloadMag();
        }
        else if (Input.GetKeyDown(KeyCode.R) && Time.time >= nextTimeToFire && magRounds <= 0)
        {
            nextTimeToReload = Time.time + 1f / reloadRate;
            ReloadMag();
        }
        //ammoText.text = magRounds.ToString() + "/" + magSize.ToString();
        //wheelAmmoText.text = magRounds.ToString() + "/" + magSize.ToString();
    }

    void MagReset()
    {
        magRounds = magSize;
        //Reset animation parameters
        nextTimeToReload = Time.time + 1f / reloadRate;
        player.SetBool("Empty", false);
    }

    void Shoot()
    {
            //Cost a bullet
            magRounds -= 1;
            //Make bullet at barrel
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            //Play shoot animation
            player.SetTrigger("Shoot");
            //Play fire sound
            //Let it die
            Destroy(bullet, 2f);

        if (magRounds <= 0)
        {

            //Execute empty mag function
            EmptyMag();
            return;
        }
    }

    void EmptyMag()
    {
        //Keep ammo at 0
        magRounds = 0;
        //Play empty animation
        player.SetBool("Empty", true);
        //Restrict shooting
        //Play empty sound effect
        return;
    }

    void ReloadMag()
    {
        //Make sure the gun is reloading
        reloading = true;
        //Reset ammo back to mag capacity
        magRounds = magSize;
        //Play reload animation
        player.SetTrigger("Reload");
        MagReset();
        return;
    }

    void Equip()
    {
        //UI.SetBool("Show", true);
        player.SetBool("Holster", false);
    }

    void Holster()
    {
        //UI.SetBool("Show", false);
        player.SetBool("Holster", true);
    }
}
