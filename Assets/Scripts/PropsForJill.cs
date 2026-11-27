using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsForJill : MonoBehaviour
{

    public Animator anim;
    void Start()
    {

    }

    public void SetTrue()
    {
        anim.SetBool("Next", true);
    }

    public void SetFalse()
    {
        anim.SetBool("Next", false);
    }
}
