using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour {
    [Tooltip("Amount of ammo to be given to player")]
    public int AmmoToRegen;

    [Tooltip("Sound upon picking up")]

    private float AnimateSpeed = 100;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, AnimateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            //collider.gameObject.transform.GetChild(0).GetChild(0).GetComponent<PlayerScript>().AddAmmo(AmmoToRegen, PickUpAudioClip);
            collider.gameObject.transform.GetComponent<PlayerScript>().AddAmmo(AmmoToRegen);

            Destroy(gameObject);
        }
    }
}
