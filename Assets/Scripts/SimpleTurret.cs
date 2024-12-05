using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurret : MonoBehaviour
{
    public Transform firePoint;    // Where the projectiles come out
    public GameObject projectilePrefab;
    public Transform target;       // The player or object to track
    public Transform turret;       // Reference to the whole turret
    public float fireInterval = 2f;
    public float projectileSpeed = 20f;
    public float rotationSpeed = 5f;  // Speed at which the turret rotates to face the target

    public float detectionRadius = 10f;  // Radius within which the turret can detect and fire at the player

    private float fireTimer;
    private Queue<GameObject> projectilePool;

    public int poolSize = 5;

    void Start()
    {
        // Initialize projectile pool
        projectilePool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectilePool.Enqueue(projectile);
        }
    }

    void Update()
    {
        if (target == null) return;

        // Check if the player is within the detection radius
        if (IsPlayerInRange())
        {
            // Rotate the turret to face the player's position
            RotateTurretTowardsPlayer();

            // Fire projectiles at intervals
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireInterval)
            {
                FireProjectile();
                fireTimer = 0f;
            }
        }
    }

    bool IsPlayerInRange()
    {
        // Calculate the distance between the turret and the player
        float distanceToPlayer = Vector3.Distance(turret.position, target.position);

        // Check if the player is within the detection radius
        return distanceToPlayer <= detectionRadius;
    }

    void RotateTurretTowardsPlayer()
    {
        // Calculate the direction to the player, ignoring the vertical (y-axis)
        Vector3 directionToTarget = new Vector3(target.position.x - firePoint.position.x, 0f, target.position.z - firePoint.position.z);

        // Check if the direction is non-zero (prevents errors when the target and firePoint are in the same position)
        if (directionToTarget.sqrMagnitude > 0.01f)
        {
            // Calculate the rotation to face the target's position
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Adjust the rotation to account for the turret's X-axis being forward in its model
            targetRotation *= Quaternion.Euler(0f, -90f, 0f); // Rotate -90 degrees on the Y-axis

            // Smoothly rotate the turret to face the target's position
            turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FireProjectile()
    {
        if (projectilePool.Count > 0)
        {
            GameObject projectile = projectilePool.Dequeue();
            projectile.transform.position = firePoint.position;

            // Fire the projectile in the direction the turret is facing, adjusted by +90 degrees
            Vector3 directionToFire = turret.forward;
            directionToFire = Quaternion.Euler(0f, 90f, 0f) * directionToFire; // Apply +90 degrees to the direction

            projectile.SetActive(true);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = directionToFire * projectileSpeed;
            }

            StartCoroutine(DeactivateAfterTime(projectile, 5f)); // Deactivate after 5 seconds
            projectilePool.Enqueue(projectile);
        }
    }

    IEnumerator DeactivateAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
