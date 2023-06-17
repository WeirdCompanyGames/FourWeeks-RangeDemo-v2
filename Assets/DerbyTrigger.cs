using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerbyTrigger : MonoBehaviour
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
            levelChanger.derbyTrigger_active = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            active = false;
            levelChanger.derbyTrigger_active = false;
        }
    }
}
