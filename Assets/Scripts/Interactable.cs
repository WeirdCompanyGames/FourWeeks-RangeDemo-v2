using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    //Interaction Type
    public enum InteractionType { NONE,TestPistolRange, TestSMGRange, TestShotgunRange, TestAssault}
    public InteractionType type;

    //Collider Trigger
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void Interact()
    {
        switch (type)
        {
            case InteractionType.TestPistolRange:
                //Add object to PickedUpItems List
                FindObjectOfType<PlayerStats>().equippedPistol = true;

                FindObjectOfType<PlayerStats>().equippedRifle = false;
                FindObjectOfType<PlayerStats>().equippedShotgun = false;

                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                FindObjectOfType<TestPistolRange>().Pickup();
                FindObjectOfType<RangeManager>().Interact();
                break;
            case InteractionType.TestSMGRange:
                //Add object to PickedUpItems List
                FindObjectOfType<PlayerStats>().equippedRifle = true;

                FindObjectOfType<PlayerStats>().equippedPistol = false;
                FindObjectOfType<PlayerStats>().equippedShotgun = false;

                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                FindObjectOfType<TestSMGRange>().Pickup();
                FindObjectOfType<RangeManager>().Interact();
                break;
            case InteractionType.TestShotgunRange:
                //Add object to PickedUpItems List
                FindObjectOfType<PlayerStats>().equippedShotgun = true;

                FindObjectOfType<PlayerStats>().equippedPistol = false;
                FindObjectOfType<PlayerStats>().equippedRifle = false;

                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                FindObjectOfType<TestShotgunRange>().Pickup();
                FindObjectOfType<RangeManager>().Interact();
                break;
            default:
                Debug.Log("NULL");
                break;

        }
    }
}
