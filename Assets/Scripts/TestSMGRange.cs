using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSMGRange : MonoBehaviour
{
    public Animator anim;

    public Transform player;
    public PlayerStats playerStats;
    
    public GameObject testPistol;
    public GameObject testSMG;
    public GameObject testShotgun;

    public void Pickup()
    {
        //Debug notification
        Debug.Log("Have fun shooting.");

        anim.Play("pistoldraw");
        testSMG.SetActive(true);

        //Enable GameObject in manager
        testPistol.SetActive(false);
        testShotgun.SetActive(false);
    }


    public void Replace()
    {
        playerStats.equippedRifle = false;
        anim.Play("pistolholster");
        testSMG.SetActive(false);
        //Disable GameObject in manager
    }
}
