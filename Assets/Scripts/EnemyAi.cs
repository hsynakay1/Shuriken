using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using Random = UnityEngine.Random;


public class EnemyAi : MonoBehaviour
{
    [SerializeField] private HealtRemember healtRemember;
    
    public Transform Eyes;
    public NavMeshAgent Agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public GameObject projectile;
    

    private void Awake()
    {
        player = GameObject.Find("Character").transform;
        //Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayerr();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        if (healtRemember.EnemyHealth <= 0 && healtRemember.EnemyArmor <= 0) DestroyEnemy();
       
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            Agent.SetDestination(walkPoint);
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude <1f)
        {
            walkPointSet = false;
        }
    }

    private void ChasePlayerr()
    {
        Agent.SetDestination(player.position);
    }

    private void SearchWalkPoint()
    {

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f ,whatIsGround ))
        {
            walkPointSet = true;
        }
    }
    private void AttackPlayer()
    {
        Agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            projectile.tag = "enemyShuriken";
            Rigidbody rb = Instantiate(projectile, Eyes.transform.position + new Vector3(0,0,1),Quaternion.identity).GetComponent<Rigidbody>();
            rb.velocity = Eyes.transform.forward * (250 * Time.deltaTime);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
   
    // ReSharper disable Unity.PerformanceAnalysis
    private void DestroyEnemy()
    {
        GameManager.Instance.DeathSomeOne();
        gameObject.SetActive(false);
        transform.position = new Vector3(Random.Range(-48f, 48f), 0.1f, Random.Range(-48f, 48f));
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(true);
        healtRemember.EnemyArmor = 100;
        healtRemember.EnemyHealth = 100;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    
}

