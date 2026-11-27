using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualDoor : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
		GetComponent<MeshCollider>().enabled = true;
		GetComponent<BoxCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && GetComponent<Animator>().GetBool("isOpened") == false)
        {
            GetComponent<Animator>().SetBool("isOpened", true);
        }
        else if ( Input.GetKey(KeyCode.E) && GetComponent<Animator>().GetBool("isOpened") == true)
		{
			GetComponent<Animator>().SetBool("isOpened", false);
		}
    }
}
