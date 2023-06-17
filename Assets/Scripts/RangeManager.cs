using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeManager : MonoBehaviour
{
    public Animator range;
    public GameObject targetGroup;

    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;

    public GameObject testPistol;
    public GameObject testSMG;
    public GameObject testShotgun;

    public PlayerStats player;

    public int roundKills;
    public int totalKills;
    public bool rangeActive;

    public void Update()
    {
        totalKills = player.enemiesKilled;
    }
    public void Interact()
    {
        if (rangeActive == false)
        {
            rangeActive = true;
            targetGroup.SetActive(true);
            range.Play("Trial_01_A");
        }
        else if (rangeActive == true)
        {
            
        }
    }

    public void Reset()
    {
        target1.gameObject.SetActive(true);
        target2.gameObject.SetActive(true);
        target3.gameObject.SetActive(true);
        target4.gameObject.SetActive(true);
    }
    public void Disable()
    {
        target1.gameObject.SetActive(false);
        target2.gameObject.SetActive(false);
        target3.gameObject.SetActive(false);
        target4.gameObject.SetActive(false);
    }
}
