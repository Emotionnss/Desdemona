using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIforButton : MonoBehaviour
{
    public AudioClip[] clip;
    public AudioSource aud;

    public void highlighted()
    {
		aud.PlayOneShot(clip[0]);
    }

    public void pressed()
    {
		aud.PlayOneShot(clip[1]);
    }
}
