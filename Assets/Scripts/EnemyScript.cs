using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Tooltip("Speed of the enemy")]
    public float MovementSpeed;

    [Tooltip("Damage on player on touch")]
    public int ContactDamage;

    [Tooltip("Health of the enemy")]
    public int HealthPoint;

    [Tooltip("Score reward for destorying enemy")]
    public int ScoreReward;

    [Tooltip("Sound upon death")]
    public AudioClip DeathAudioClip;

    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;

    private PlayerScript playerScript;
    private Animator anim;
    public static Animator spec;

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (GetComponent<Animator>() == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
        else
        {
            spec = GetComponent<Animator>();
        }

        //playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerTransform != null && !GameManager.Instance.isGameOver)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }

        //playerScript.GetHealth();
    }

    public void OnHit(int damage)
    {
        HealthPoint -= damage;

        if (HealthPoint <= 0)
        {
            navMeshAgent.speed = 0;

            if (spec != null)
            {
                SpecialDie();
            }
            else
            {
                StartCoroutine(Dieded());
            }
        }
    }

    public IEnumerator Dieded()
    {
        GameManager.Instance.UpdateScore(ScoreReward, DeathAudioClip);
        if (anim != null)
        {
            anim.SetBool("dead", true); //Error

            yield return new WaitForSeconds(2);
            anim.enabled = false;
        }
        Destroy(gameObject, 2.5f);
    }

    public void SpecialDie()
    {
        GameManager.Instance.UpdateScore(ScoreReward, DeathAudioClip);
        spec.SetBool("Die", true);
    }

    public void Attack()
    {
        anim.SetBool("Hit", true);
        navMeshAgent.speed = 0;
    }
    public void ResetWalk()
    {
        anim.SetBool("Hit", false);
        navMeshAgent.speed = 3.5f;
    }

    public void ByeBye()
    {
        Destroy(gameObject);
    }
}
