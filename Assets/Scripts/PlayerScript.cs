using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour
{
    [Tooltip("Shooting rate of the player")]
    public float ShootingRate;

    [Tooltip("Damage on enemy on each hit")]
    public int ShootingDamage;

    [Tooltip("Damage speed with contact with enemy")]
    public int DamageRate;

    [Tooltip("Starting health of the enemy")]
    public int HealthPoint;

    [Tooltip("Starting ammo of the enemy")]
    public int AmmoCount;

    [Tooltip("Shooting sound effect")]
    public AudioClip ShootingAudioClip;

    private Rigidbody rb = null;
    private Vector3 moveDirection = Vector3.zero;
    private bool canShoot;
    private bool canDamage;
    private AudioSource audioSource;
    private GameObject camera = null;
    private float move;
    public Animator anim;
    public GameObject    hurt;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        canShoot = true;
        canDamage = true;
        audioSource.clip = ShootingAudioClip;
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        GameManager.Instance.UpdateAmmo(AmmoCount);

        GameManager.Instance.UpdateHealth(HealthPoint);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Debug.DrawRay(camera.transform.position, camera.transform.forward * 100, Color.green);
        if (GameManager.Instance.isGameOver)
            return;

        if (transform.position.y < -3)
        {
            GameManager.Instance.SetGameOver(false); //SetGameOver == win or lose/ win means true/ lose means false/
            Destroy(gameObject);
        }
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && AmmoCount > 0)
        {
            StartCoroutine(SpawnBullet());
        }
    }

    private IEnumerator SpawnBullet()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position + transform.forward, camera.transform.forward, out hit, Mathf.Infinity))
        {
            print(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag.Equals("Head"))
            {
                Debug.Log("HEAD!");
                hit.collider.gameObject.GetComponentInParent<EnemyScript>().OnHit(ShootingDamage * 4);
            }
            else if (hit.collider.gameObject.tag.Equals("Body") || hit.collider.gameObject.tag.Equals("Enemy"))
            {
                if (hit.collider.gameObject.GetComponentInParent<EnemyScript>() == null)
                {
                    hit.collider.gameObject.GetComponent<EnemyScript>().OnHit(ShootingDamage);
                }
                else
                {
                    hit.collider.gameObject.GetComponentInParent<EnemyScript>().OnHit(ShootingDamage);
                }
            }
        }

        audioSource.PlayOneShot(ShootingAudioClip);
        GameManager.Instance.UpdateAmmo(--AmmoCount);

        canShoot = false;
        //wait for some time
        yield return new WaitForSeconds(ShootingRate);

        canShoot = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && canDamage)
        {
            StartCoroutine(GetDamage(collision));
        }
    }


    private IEnumerator GetDamage(Collider collision)
    {
        EnemyScript enemyScript = collision.gameObject.GetComponentInParent<EnemyScript>(); //Changed
        HealthPoint -= enemyScript.ContactDamage;
        GameManager.Instance.UpdateHealth(HealthPoint);
        StartCoroutine(damaged());

        if (collision.gameObject.GetComponent<Animator>() != null)
        {
            enemyScript.Attack();
        }


        if (HealthPoint <= 0)
        {
            Dead();
        }

        canDamage = false;
        //wait for some time
        yield return new WaitForSeconds(DamageRate);

        canDamage = true;
    }

    private IEnumerator damaged()
    {
        hurt.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hurt.SetActive(false);
    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    public void AddAmmo(int ammo)
    {
        AmmoCount += ammo;
        GameManager.Instance.UpdateAmmo(AmmoCount);
    }

    public void AddHealth(int health)
    {
        HealthPoint += health;
        GameManager.Instance.UpdateHealth(HealthPoint);
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
        float front = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Forward", front);
        float sideways = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Side", sideways);
    }
}
