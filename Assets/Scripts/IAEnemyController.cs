using UnityEngine;
using UnityEngine.AI;

public class IAEnemyController : MonoBehaviour
{
    public Transform player;
    public float visionRange = 15f;
    public float shootRange = 10f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    private NavMeshAgent agent;
    private float fireCooldown;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fireCooldown = 0f;
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Check if player is within vision range and visible
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= visionRange && CanSeePlayer(directionToPlayer))
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer <= shootRange && fireCooldown <= 0f)
            {
                ShootAtPlayer();
                fireCooldown = fireRate;
            }
        }
        else
        {
            agent.ResetPath(); // Stop if player not visible
        }
    }

    bool CanSeePlayer(Vector3 directionToPlayer)
    {
        Ray ray = new Ray(transform.position + Vector3.up, directionToPlayer.normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, visionRange))
        {
            if (hit.transform == player)
                return true;
        }
        return false;
    }

    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = (player.position - firePoint.position).normalized * bulletSpeed;
    }
}
