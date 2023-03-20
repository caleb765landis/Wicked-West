using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CodeMonkey.HealthSystemCM;

public class Enemy : MonoBehaviour, IGetHealthSystem
{

    public Animator goblinanim;

    static public float maxHealth = 100f;
    private HealthSystem healthSystemComponent;

    public NavMeshAgent agent;

    public Transform player;
    public GameObject playerObject;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float damageDealt = 10f;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
		{
			float damage = other.GetComponent<Bullet>().bulletDamage;
            healthSystemComponent.Damage(damage);
            print(healthSystemComponent.GetHealth());
            Destroy(other.gameObject);
		}
    }

    private void Awake()
    {
        player = GameObject.Find("PlayerModels").transform;
        agent = GetComponent<NavMeshAgent>();

        healthSystemComponent = new HealthSystem(maxHealth);
        healthSystemComponent.OnDead += HealthSystem_OnDead;

    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

       
    }

    private void Patroling()
    {
        goblinanim.SetBool("isWalking", true);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        goblinanim.SetBool("isWalking", true);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        goblinanim.SetBool("isWalking", false);

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            AttackAction();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public void AttackAction(){
        //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        //rb.AddForce(transform.up * 8f, ForceMode.Impulse);

        //player.GetHealthSystem().Damage(damageDealt);
        player.gameObject.GetComponent<PlayerController>().GetHealthSystem().Damage(damageDealt);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(this.transform.parent.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystemComponent;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        DestroyEnemy();
    }
}
