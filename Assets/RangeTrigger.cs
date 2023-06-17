using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTrigger : MonoBehaviour
{
    public LevelChanger levelChanger;
    public bool active;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            active = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        active = true;
        if (active == true)
        {
            levelChanger.rangeTrigger_active = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            active = false;
            levelChanger.rangeTrigger_active = false;
        }
    }
}
