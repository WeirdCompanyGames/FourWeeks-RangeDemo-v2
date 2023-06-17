using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("Detection Fields")]
    //Detection point
    public Transform detectPoint;
    //Detection radius
    private const float detectRadius = 0.375f;
    //Detection Layer
    public LayerMask detectLayer;
    //Cached Trigger Object
    public GameObject detectedObject;
    //List of picked items
    public List<GameObject> pickedUpItems= new List<GameObject>();

    public float interactRate = 4f;
    public float nextTimeToInteract = 0f;
    public bool lockInput;

    // Update is called once per frame
    void Update()
    {
        if (lockInput == false)
        {
                if (InteractInput() && Time.time >= nextTimeToInteract)
                {   
                if (DetectObject())
                {
                
                    nextTimeToInteract = Time.time + 1f / interactRate;
                    detectedObject.GetComponent<Interactable>().Interact();
                    Debug.Log(detectedObject.name);
                    return;
                } else
                {
                    return;
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(detectPoint.position, detectRadius);
    }

    bool InteractInput()
    {
        {
        return Input.GetKeyDown(KeyCode.E);
        }
    }

    bool DetectObject()
    {

        Collider2D obj = Physics2D.OverlapCircle(detectPoint.position, detectRadius, detectLayer);
        if (obj == null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    public void PickUpItem(GameObject item)
    {

        pickedUpItems.Add(item);
    }
}
