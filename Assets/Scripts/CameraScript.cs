using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Camera cam;

    [SerializeField] private Vector3 Default;
    [SerializeField] private Vector3 AimDown;

    [SerializeField] private Vector3 phoneUp;
    [SerializeField] private Vector3 phoneDown;

    public static AudioSource bgm;

    public GameObject arms;

    public GameObject phone;

    void Start()
    {
        cam = GetComponent<Camera>();
        bgm = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AimDownSight();
        Checking();
    }

    private void Checking()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            phone.transform.localPosition = Vector3.Lerp(phone.transform.localPosition, phoneUp, 5 * Time.deltaTime);
        }
        else
        {
            phone.transform.localPosition = Vector3.Lerp(phone.transform.localPosition, phoneDown, 5 * Time.deltaTime);
        }
    }

    private void AimDownSight()
    {
        if (Input.GetMouseButton(1))
        {
            arms.transform.localPosition = Vector3.Lerp(arms.transform.localPosition, AimDown, 7.5f * Time.deltaTime);
            cam.fieldOfView = 30;
        }
        else
        {
            arms.transform.localPosition = Vector3.Lerp(arms.transform.localPosition, Default, 7.5f * Time.deltaTime);
            cam.fieldOfView = 60;
        }
    }
}
