using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 2f;

    public Animator anim;
    public Rigidbody2D rb;
    public Camera cam;
    public GameObject reticle;
    public GameObject player;

    public Vector2 movement;
    public bool sprinting;
    Vector2 mousePos;

    void Start()
    {
        Cursor.visible = false;
        moveSpeed = 4;
    }
    // Update is called once per frame
    public void Update()
    {

        //Input Variables
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Mouse Input Variables
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        reticle.transform.position = mousePos;
    }

    public void FixedUpdate()
    {
        //Physical Movement 
        rb.MovePosition(position: rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
        if (Input.GetButton("Sprint"))
        {
            moveSpeed = 7f;
        }else
        {
            moveSpeed = 4f;
        }
            movement.Normalize();

        //Animation
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);

        //Character-to-Cursor Directional Aiming (based on angle)
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }

}
