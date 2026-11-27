using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool isTriggered;

    private ActivatableDoor activatableDoor;
    [SerializeField] private GameObject FinalWall;

    // Use this for initialization
    void Start()
    {
        FinalWall.SetActive(false);
        isTriggered = false;
    }

    public void SetActivatableDoor(ActivatableDoor _activatableDoor)
    {
        activatableDoor = _activatableDoor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && gameObject.name != "Last")
        {
            isTriggered = true;
            gameObject.SetActive(false);
        }
        else if (other.gameObject.tag.Equals("Player") && gameObject.name == "Last")
        {
            print("Hits");
            if (FinalWall != null)
            {
                print("Hits2");
                FinalWall.SetActive(true);
                isTriggered = true;
                gameObject.SetActive(false);
            }
        }
    }
}
