using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShotgunRange : MonoBehaviour
{
    public Animator anim;

    public Transform player;
    public PlayerStats playerStats;
    
    public GameObject testShotgun;
    public GameObject testPistol;
    public GameObject testSMG;

    public void Pickup()
    {
        //Debug notification
        Debug.Log("Have fun shooting.");

        anim.Play("pistoldraw");
        testShotgun.SetActive(true);

        //Enable GameObject in manager
        testPistol.SetActive(false);
        testSMG.SetActive(false);
    }


    public void Replace()
    {
        playerStats.equippedShotgun = false;
        anim.Play("pistolholster");
        testShotgun.SetActive(false);
        //Disable GameObject in manager
    }
}
