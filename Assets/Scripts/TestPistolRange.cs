using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPistolRange : MonoBehaviour
{
    public Animator anim;

    public Transform player;
    public PlayerStats playerStats;

    public GameObject testPistol;
    public GameObject testSMG;
    public GameObject testShotgun;

    public bool isEnabled;

    public void Pickup()
    {
        if(testPistol == true)
        {
            //Debug notification
            Debug.Log("Have fun shooting.");

            anim.Play("pistoldraw");
            testPistol.SetActive(true);
            isEnabled = true;

            //Enable GameObject in manager
            testSMG.SetActive(false);
            testShotgun.SetActive(false);
        }
        else
        {
            Replace();
        }
        
    }


    public void Replace()
    {
        playerStats.equippedPistol = false;
        anim.Play("pistolholster");
        testPistol.SetActive(false);
        //Disable GameObject in manager
    }
}
