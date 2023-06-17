using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : MonoBehaviour
{
    public Animator anim;
    public BoxCollider2D punchCollider;

    [Range(0.1f, 1.0f)]
    public float punchDelay;
    public int punchDamage;
    public int punchPicker;
    public bool fighting;
    public bool punching;
    public bool blocking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Aim"))
        {
            fighting = true;
            anim.SetBool("Fighting", true);
            if (fighting == true && Input.GetButtonDown("Fire"))
            {
                StartCoroutine(Punch());
            }
        }
        else
        {
            fighting = false;
            anim.SetBool("Fighting", false);
        }
        
    }

    public IEnumerator Punch()
    {
        punchPicker = Random.Range(1, 6);
        anim.SetInteger("Punch", punchPicker);
        anim.SetTrigger("Punching");
        punching = true;

        yield return new WaitForSeconds(punchDelay);
        punchPicker = 0;
        anim.SetInteger("Punch", punchPicker);
        punching = false;

    }
}
